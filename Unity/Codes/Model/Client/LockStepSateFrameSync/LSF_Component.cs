using System.Collections.Generic;

namespace ET
{
    public class LSF_Component : Entity
    {
        /// <summary>
        /// 整局游戏的Cmd记录，用于断线重连
        /// </summary>
        public Dictionary<uint, Queue<ALSF_Cmd>> WholeCmds = new Dictionary<uint, Queue<ALSF_Cmd>>(8192);

        /// <summary>
        /// 将要处理的命令列表
        /// </summary>
        public SortedDictionary<uint, Queue<ALSF_Cmd>> FrameCmdsToHandle = new SortedDictionary<uint, Queue<ALSF_Cmd>>();

        /// <summary>
        /// 将要发送的命令列表
        /// </summary>
        public Dictionary<uint, Queue<ALSF_Cmd>> FrameCmdsToSend = new Dictionary<uint, Queue<ALSF_Cmd>>(64);

        /// <summary>
        /// 用于帧同步的FixedUpdate，需要注意的是，这个FixedUpdate与框架层的是不搭嘎的
        /// </summary>
        public FixedUpdate FixedUpdate;

        /// <summary>
        /// 开启模拟
        /// </summary>
        public bool StartSync;

        /// <summary>
        /// 当前帧数
        /// </summary>
        public uint CurrentFrame;

        /// <summary>
        /// 服务器缓冲帧时长，按帧为单位，这里锁定为1帧，也就是33ms
        /// </summary>
        public uint BufferFrame = 1;

    }
}