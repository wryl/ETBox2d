namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class M2C_CreateMyUnitHandler : AMHandler<M2C_CreateMyUnit>
	{
		protected override async ETTask Run(Session session, M2C_CreateMyUnit message)
		{
			// 通知场景切换协程继续往下走
			session.DomainScene().GetComponent<ObjectWait>().Notify(new WaitType.Wait_CreateMyUnit() {Message = message});
			await ETTask.CompletedTask;
		}
	}
	[MessageHandler(SceneType.Zone)]
	public class M2C_CreateMyUnit2DHandler : AMHandler<M2C_CreateMyUnit2D>
	{
		protected override async ETTask Run(Session session, M2C_CreateMyUnit2D message)
		{
			// 通知场景切换协程继续往下走
			session.DomainScene().GetComponent<ObjectWait>().Notify(new WaitType.Wait_CreateMyUnit2D() {Message = message});
			await ETTask.CompletedTask;
		}
	}
}
