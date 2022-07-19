
using System;
using System.Numerics;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class P2PTargetMessageHandler : AMHandler<P2PTargetMessage>
	{
		protected override async ETTask Run(Session session, P2PTargetMessage message)
		{
			((P2PService)session.AService).ChangeAddress(session.Id,NetworkHelper.ToIPEndPoint(message.TargetAddress),message.RemoteConn);
			await TimerComponent.Instance.WaitAsync(3000);
			session.AddComponent<PingComponent>();
			await ETTask.CompletedTask;
		}
	}
	[MessageHandler(SceneType.Client)]
	public class M2C_StartP2PHandler : AMHandler<M2C_StartP2P>
	{
		protected override async ETTask Run(Session session, M2C_StartP2P message)
		{
			P2PHelper.Login(session.ClientScene(), message.Address);
			await ETTask.CompletedTask;
		}
	}
	[MessageHandler(SceneType.Client)]
	public class C2G_PingHandler : AMRpcHandler<C2G_Ping, G2C_Ping>
	{
		protected override async ETTask Run(Session session, C2G_Ping request, G2C_Ping response, Action reply)
		{
			response.Time = TimeHelper.ServerNow();
			Log.Debug("p2p ping");
			reply();
			await ETTask.CompletedTask;
		}
	}
}
