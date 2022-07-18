
using System.Numerics;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class P2PTargetMessageHandler : AMHandler<P2PTargetMessage>
	{
		protected override async ETTask Run(Session session, P2PTargetMessage message)
		{
			((P2PService)session.AService).ChangeAddress(session.Id,NetworkHelper.ToIPEndPoint(message.TargetAddress),message.RemoteConn);
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
}
