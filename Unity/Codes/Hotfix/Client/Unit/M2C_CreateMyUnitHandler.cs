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
	[MessageHandler(SceneType.Client)]
	public class M2C_CreateMyUnit2DHandler : AMHandler<M2C_CreateMyUnit2D>
	{
		protected override async ETTask Run(Session session, M2C_CreateMyUnit2D message)
		{
			// 通知场景切换协程继续往下走
			session.DomainScene().GetComponent<ObjectWait>().Notify(new WaitType.Wait_CreateMyUnit2D() {Message = message});
			await ETTask.CompletedTask;
		}
	}
	[MessageHandler(SceneType.Client)]
	public class M2C_CreateUnit2DsHandler : AMHandler<M2C_CreateUnit2Ds>
	{
		protected override async ETTask Run(Session session, M2C_CreateUnit2Ds message)
		{
			Scene currentScene = session.DomainScene().CurrentScene();
			Unit2DComponent unitComponent = currentScene.GetComponent<Unit2DComponent>();
			
			foreach (UnitInfo unitInfo in message.Units)
			{
				if (unitComponent.Get(unitInfo.UnitId) != null)
				{
					continue;
				}
				Unit2D unit = UnitFactory.Create2D(currentScene, unitInfo,false);
			}
			await ETTask.CompletedTask;
		}
	}
}
