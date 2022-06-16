using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ET
{
 

    public sealed class P2PService: AService
    {
        // P2PService创建的时间
        private readonly long startTime;

        // 当前时间 - P2PService创建的时间, 线程安全
        public uint TimeNow
        {
            get
            {
                return (uint) (TimeHelper.ClientNow() - this.startTime);
            }
        }

        private Socket socket;


#region 回调方法

        static P2PService()
        {
            //Kcp.KcpSetLog(KcpLog);
            Kcp.KcpSetoutput(KcpOutput);
        }

        private static readonly byte[] logBuffer = new byte[1024];

#if ENABLE_IL2CPP
		[AOT.MonoPInvokeCallback(typeof(KcpOutput))]
#endif
        private static void KcpLog(IntPtr bytes, int len, IntPtr kcp, IntPtr user)
        {
            try
            {
                Marshal.Copy(bytes, logBuffer, 0, len);
                Log.Info(logBuffer.ToStr(0, len));
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

#if ENABLE_IL2CPP
		[AOT.MonoPInvokeCallback(typeof(KcpOutput))]
#endif
        private static int KcpOutput(IntPtr bytes, int len, IntPtr kcp, IntPtr user)
        {
            try
            {
                if (kcp == IntPtr.Zero)
                {
                    return 0;
                }

                if (!P2PChannel.KcpPtrChannels.TryGetValue(kcp, out P2PChannel p2PChannel))
                {
                    return 0;
                }
                
                p2PChannel.Output(bytes, len);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return len;
            }

            return len;
        }

#endregion

        public P2PService(ThreadSynchronizationContext threadSynchronizationContext, IPEndPoint ipEndPoint, ServiceType serviceType)
        {
            this.ServiceType = serviceType;
            this.ThreadSynchronizationContext = threadSynchronizationContext;
            this.startTime = TimeHelper.ClientNow();
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                this.socket.SendBufferSize = Kcp.OneM * 64;
                this.socket.ReceiveBufferSize = Kcp.OneM * 64;
            }

            this.socket.Bind(ipEndPoint);
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                const uint IOC_IN = 0x80000000;
                const uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                this.socket.IOControl((int) SIO_UDP_CONNRESET, new[] { Convert.ToByte(false) }, null);
            }
        }

        public P2PService(ThreadSynchronizationContext threadSynchronizationContext, ServiceType serviceType)
        {
            this.ServiceType = serviceType;
            this.ThreadSynchronizationContext = threadSynchronizationContext;
            this.startTime = TimeHelper.ClientNow();
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            // 作为客户端不需要修改发送跟接收缓冲区大小
            this.socket.Bind(new IPEndPoint(IPAddress.Any, 0));

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                const uint IOC_IN = 0x80000000;
                const uint IOC_VENDOR = 0x18000000;
                uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                this.socket.IOControl((int) SIO_UDP_CONNRESET, new[] { Convert.ToByte(false) }, null);
            }
        }

        public void ChangeAddress(long id, IPEndPoint address)
        {
            P2PChannel P2PChannel = this.Get(id);
            if (P2PChannel == null)
            {
                return;
            }

            Log.Info($"channel change address: {id} {address}");
            P2PChannel.RemoteAddress = address;
        }


        // 保存所有的channel
        private readonly Dictionary<long, P2PChannel> idChannels = new Dictionary<long, P2PChannel>();
        private readonly Dictionary<long, P2PChannel> localConnChannels = new Dictionary<long, P2PChannel>();
        private readonly Dictionary<long, P2PChannel> waitConnectChannels = new Dictionary<long, P2PChannel>();

        private readonly byte[] cache = new byte[8192];
        private EndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);

        // 下帧要更新的channel
        private readonly HashSet<long> updateChannels = new HashSet<long>();

        // 下次时间更新的channel
        private readonly MultiMap<long, long> timeId = new MultiMap<long, long>();

        private readonly List<long> timeOutTime = new List<long>();

        // 记录最小时间，不用每次都去MultiMap取第一个值
        private long minTime;

        private List<long> waitRemoveChannels = new List<long>();

        public override bool IsDispose()
        {
            return this.socket == null;
        }

        public override void Dispose()
        {
            foreach (long channelId in this.idChannels.Keys.ToArray())
            {
                this.Remove(channelId);
            }

            this.socket.Close();
            this.socket = null;
        }
        
        private IPEndPoint CloneAddress()
        {
            IPEndPoint ip = (IPEndPoint) this.ipEndPoint;
            return new IPEndPoint(ip.Address, ip.Port);
        }

        private void Recv()
        {
            if (this.socket == null)
            {
                return;
            }

            while (socket != null && this.socket.Available > 0)
            {
                int messageLength = this.socket.ReceiveFrom(this.cache, ref this.ipEndPoint);

                // 长度小于1，不是正常的消息
                if (messageLength < 1)
                {
                    continue;
                }

                // accept
                byte flag = this.cache[0];
                    
                // conn从100开始，如果为1，2，3则是特殊包
                uint remoteConn = 0;
                uint localConn = 0;
                
                try
                {
                    P2PChannel p2PChannel = null;
                    switch (flag)
                    {
                        case KcpProtocalType.SYN: // accept
                        {
                            // 长度!=5，不是SYN消息
                            if (messageLength < 9)
                            {
                                break;
                            }

                            remoteConn = BitConverter.ToUInt32(this.cache, 1);


                            remoteConn = BitConverter.ToUInt32(this.cache, 1);
                            localConn = BitConverter.ToUInt32(this.cache, 5);

                            this.waitConnectChannels.TryGetValue(remoteConn, out p2PChannel);
                            if (p2PChannel == null)
                            {
                                localConn = CreateRandomLocalConn();
                                // 已存在同样的localConn，则不处理，等待下次sync
                                if (this.localConnChannels.ContainsKey(localConn))
                                {
                                    break;
                                }
                                long id = this.CreateAcceptChannelId(localConn);
                                if (this.idChannels.ContainsKey(id))
                                {
                                    break;
                                }

                                p2PChannel = new P2PChannel(id, localConn, remoteConn, this.socket, this.CloneAddress(), this);
                                this.idChannels.Add(p2PChannel.Id, p2PChannel);
                                this.waitConnectChannels.Add(p2PChannel.RemoteConn, p2PChannel); // 连接上了或者超时后会删除
                                this.localConnChannels.Add(p2PChannel.LocalConn, p2PChannel);


                                IPEndPoint realEndPoint = p2PChannel.RealAddress == null? p2PChannel.RemoteAddress : NetworkHelper.ToIPEndPoint(p2PChannel.RealAddress);
                                this.OnAccept(p2PChannel.Id, realEndPoint);
                            }
                            if (p2PChannel.RemoteConn != remoteConn)
                            {
                                break;
                            }

                            try
                            {
                                byte[] buffer = this.cache;
                                buffer.WriteTo(0, KcpProtocalType.ACK);
                                buffer.WriteTo(1, p2PChannel.LocalConn);
                                buffer.WriteTo(5, p2PChannel.RemoteConn);
                                Log.Info($"P2PService syn: {p2PChannel.Id} {remoteConn} {localConn}");
                                this.socket.SendTo(buffer, 0, 9, SocketFlags.None, p2PChannel.RemoteAddress);
                            }
                            catch (Exception e)
                            {
                                Log.Error(e);
                                p2PChannel.OnError(ErrorCore.ERR_SocketCantSend);
                            }

                            break;
                        }
                        case KcpProtocalType.ACK: // connect返回
                            // 长度!=9，不是connect消息
                            if (messageLength != 9)
                            {
                                break;
                            }

                            remoteConn = BitConverter.ToUInt32(this.cache, 1);
                            localConn = BitConverter.ToUInt32(this.cache, 5);
                            p2PChannel = this.GetByLocalConn(localConn);
                            if (p2PChannel != null)
                            {
                                Log.Info($"P2PService ack: {p2PChannel.Id} {remoteConn} {localConn}");
                                p2PChannel.RemoteConn = remoteConn;
                                p2PChannel.HandleConnnect();
                            }

                            break;
                        case KcpProtocalType.FIN: // 断开
                            // 长度!=13，不是DisConnect消息
                            if (messageLength != 13)
                            {
                                break;
                            }

                            remoteConn = BitConverter.ToUInt32(this.cache, 1);
                            localConn = BitConverter.ToUInt32(this.cache, 5);
                            int error = BitConverter.ToInt32(this.cache, 9);

                            // 处理chanel
                            p2PChannel = this.GetByLocalConn(localConn);
                            if (p2PChannel == null)
                            {
                                break;
                            }
                            
                            // 校验remoteConn，防止第三方攻击
                            if (p2PChannel.RemoteConn != remoteConn)
                            {
                                break;
                            }
                            
                            Log.Info($"P2PService recv fin: {p2PChannel.Id} {localConn} {remoteConn} {error}");
                            p2PChannel.OnError(ErrorCore.ERR_PeerDisconnect);

                            break;
                        case KcpProtocalType.MSG: // 断开
                            // 长度<9，不是Msg消息
                            if (messageLength < 9)
                            {
                                break;
                            }
                            // 处理chanel
                            remoteConn = BitConverter.ToUInt32(this.cache, 1);
                            localConn = BitConverter.ToUInt32(this.cache, 5);

                            p2PChannel = this.GetByLocalConn(localConn);
                            if (p2PChannel == null)
                            {
                                // 通知对方断开
                                this.Disconnect(localConn, remoteConn, ErrorCore.ERR_KcpNotFoundChannel, (IPEndPoint) this.ipEndPoint, 1);
                                break;
                            }
                            
                            // 校验remoteConn，防止第三方攻击
                            if (p2PChannel.RemoteConn != remoteConn)
                            {
                                break;
                            }
                            
                            p2PChannel.HandleRecv(this.cache, 5, messageLength - 5);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"P2PService error: {flag} {remoteConn} {localConn}\n{e}");
                }
            }
        }

        public P2PChannel Get(long id)
        {
            P2PChannel channel;
            this.idChannels.TryGetValue(id, out channel);
            return channel;
        }
        
        private P2PChannel GetByLocalConn(uint localConn)
        {
            P2PChannel channel;
            this.localConnChannels.TryGetValue(localConn, out channel);
            return channel;
        }

        protected override void Get(long id, IPEndPoint address)
        {
            if (this.idChannels.TryGetValue(id, out P2PChannel P2PChannel))
            {
                return;
            }

            try
            {
                // 低32bit是localConn
                uint localConn = (uint) ((ulong) id & uint.MaxValue);
                P2PChannel = new P2PChannel(id, localConn, this.socket, address, this);
                this.idChannels.Add(id, P2PChannel);
                this.localConnChannels.Add(P2PChannel.LocalConn, P2PChannel);
            }
            catch (Exception e)
            {
                Log.Error($"P2PService get error: {id}\n{e}");
            }
        }

        public override void Remove(long id)
        {
            if (!this.idChannels.TryGetValue(id, out P2PChannel P2PChannel))
            {
                return;
            }
            Log.Info($"P2PService remove channel: {id} {P2PChannel.LocalConn} {P2PChannel.RemoteConn}");
            this.idChannels.Remove(id);
            this.localConnChannels.Remove(P2PChannel.LocalConn);
            if (this.waitConnectChannels.TryGetValue(P2PChannel.RemoteConn, out P2PChannel waitChannel))
            {
                if (waitChannel.LocalConn == P2PChannel.LocalConn)
                {
                    this.waitConnectChannels.Remove(P2PChannel.RemoteConn);
                }
            }
            P2PChannel.Dispose();
        }

        private void Disconnect(uint localConn, uint remoteConn, int error, IPEndPoint address, int times)
        {
            try
            {
                if (this.socket == null)
                {
                    return;
                }

                byte[] buffer = this.cache;
                buffer.WriteTo(0, KcpProtocalType.FIN);
                buffer.WriteTo(1, localConn);
                buffer.WriteTo(5, remoteConn);
                buffer.WriteTo(9, (uint) error);
                for (int i = 0; i < times; ++i)
                {
                    this.socket.SendTo(buffer, 0, 13, SocketFlags.None, address);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Disconnect error {localConn} {remoteConn} {error} {address} {e}");
            }
            
            Log.Info($"channel send fin: {localConn} {remoteConn} {address} {error}");
        }
        
        protected override void Send(long channelId, long actorId, MemoryStream stream)
        {
            P2PChannel channel = this.Get(channelId);
            if (channel == null)
            {
                return;
            }
            channel.Send(actorId, stream);
        }

        // 服务端需要看channel的update时间是否已到
        public void AddToUpdateNextTime(long time, long id)
        {
            if (time == 0)
            {
                this.updateChannels.Add(id);
                return;
            }
            if (time < this.minTime)
            {
                this.minTime = time;
            }
            this.timeId.Add(time, id);
        }

        public override void Update()
        {
            this.Recv();
            
            this.TimerOut();

            foreach (long id in updateChannels)
            {
                P2PChannel P2PChannel = this.Get(id);
                if (P2PChannel == null)
                {
                    continue;
                }

                if (P2PChannel.Id == 0)
                {
                    continue;
                }

                P2PChannel.Update();
            }

            this.updateChannels.Clear();
            
            this.RemoveConnectTimeoutChannels();
        }

        private void RemoveConnectTimeoutChannels()
        {
            waitRemoveChannels.Clear();
            foreach (long channelId in this.waitConnectChannels.Keys)
            {
                this.waitConnectChannels.TryGetValue(channelId, out P2PChannel P2PChannel);
                if (P2PChannel == null)
                {
                    Log.Error($"RemoveConnectTimeoutChannels not found P2PChannel: {channelId}");
                    continue;
                }

                // 连接上了要马上删除
                if (P2PChannel.IsConnected)
                {
                    waitRemoveChannels.Add(channelId);
                }

                // 10秒连接超时
                if (this.TimeNow > P2PChannel.CreateTime + 10 * 1000)
                {
                    waitRemoveChannels.Add(channelId);
                }
            }

            foreach (long channelId in waitRemoveChannels)
            {
                this.waitConnectChannels.Remove(channelId);
            }
        }

        // 计算到期需要update的channel
        private void TimerOut()
        {
            if (this.timeId.Count == 0)
            {
                return;
            }

            uint timeNow = this.TimeNow;

            if (timeNow < this.minTime)
            {
                return;
            }

            this.timeOutTime.Clear();

            foreach (KeyValuePair<long, List<long>> kv in this.timeId)
            {
                long k = kv.Key;
                if (k > timeNow)
                {
                    minTime = k;
                    break;
                }

                this.timeOutTime.Add(k);
            }

            foreach (long k in this.timeOutTime)
            {
                foreach (long v in this.timeId[k])
                {
                    this.updateChannels.Add(v);
                }

                this.timeId.Remove(k);
            }
        }
    }
}