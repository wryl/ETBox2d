using System;

namespace ET.Server
{

	[MessageHandler(SceneType.Gate)]
	public class C2G_Enter2DHandler : AMRpcHandler<C2G_Enter2D, G2C_Enter2D>
	{
		protected override async ETTask Run(Session session, C2G_Enter2D request, G2C_Enter2D response, Action reply)
		{
			Player player = session.GetComponent<SessionPlayerComponent>().GetMyPlayer();

			// 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
			GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
			gateMapComponent.Scene = await SceneFactory.Create(gateMapComponent, "GateBox2d", SceneType.Box2dWorld);

			Scene scene = gateMapComponent.Scene;
			
			// 这里可以从DB中加载Unit
			Unit2D unit = Server.UnitFactory.Create2D(scene, player.Id, UnitType.Player);
			unit.AddComponent<UnitGateComponent, long>(session.InstanceId);
			StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Box2dWorld1");
			response.MyId = unit.Id;
			reply();
			
			// 开始传送
			await Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);
		}
		public static async ETTask Transfer(Unit2D unit, long sceneInstanceId, string sceneName)
		{
			// 通知客户端开始切场景
			M2C_StartSceneChange m2CStartSceneChange = new M2C_StartSceneChange() {SceneInstanceId = sceneInstanceId, SceneName = sceneName};
			MessageHelper.SendToClient(unit, m2CStartSceneChange);
            
			M2M_Unit2DTransferRequest request = new M2M_Unit2DTransferRequest();
			request.Unit = unit;
			foreach (Entity entity in unit.Components.Values)
			{
				if (entity is ITransfer)
				{
					request.Entitys.Add(entity);
				}
			}
			// 删除Mailbox,让发给Unit的ActorLocation消息重发
			unit.RemoveComponent<MailBoxComponent>();
			// location加锁
			long oldInstanceId = unit.InstanceId;
			await LocationProxyComponent.Instance.Lock(unit.Id, unit.InstanceId);
			M2M_Unit2DTransferResponse response = await ActorMessageSenderComponent.Instance.Call(sceneInstanceId, request) as M2M_Unit2DTransferResponse;
			await LocationProxyComponent.Instance.UnLock(unit.Id, oldInstanceId, response.NewInstanceId);
			unit.Domain.Dispose();
		}
	}
}