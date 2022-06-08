using System;
using UnityEngine;

namespace ET.Server
{
	[Event(SceneType.Map)]
	public class ChangePosition2D: AEvent<Unit, ET.EventType.ChangePosition2D>
	{
		protected override async ETTask Run(Unit unit, ET.EventType.ChangePosition2D args)
		{
			Vector3 oldPos = args.OldPos;
			await ETTask.CompletedTask;
		}
	}
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
			StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Box2DWorld1");
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
			unit.Dispose();
		}
	}

	[ActorMessageHandler(SceneType.Box2dWorld)]
	public class M2M_Unit2DTransferRequestHandler: AMActorRpcHandler<Scene, M2M_Unit2DTransferRequest, M2M_Unit2DTransferResponse>
	{
		protected override async ETTask Run(Scene scene, M2M_Unit2DTransferRequest request, M2M_Unit2DTransferResponse response, Action reply)
		{
			await ETTask.CompletedTask;
			Unit2DComponent unitComponent = scene.GetComponent<Unit2DComponent>();
			Unit2D unit = request.Unit;
			unitComponent.AddChild(unit);
			unitComponent.Add(unit);
			foreach (Entity entity in request.Entitys)
			{
				unit.AddComponent(entity);
			}
			unit.Position = new Vector3(5, 10, 0);

			unit.AddComponent<MailBoxComponent>();
			unit.AddComponent<Player2D>();
			// 通知客户端创建My Unit
			M2C_CreateMyUnit m2CCreateUnits = new M2C_CreateMyUnit();
			m2CCreateUnits.Unit = Server.Unit2DHelper.CreateUnitInfo(unit);
			MessageHelper.SendToClient(unit, m2CCreateUnits);
			response.NewInstanceId = unit.InstanceId;
			reply();
		}

		
	}
	[FriendClass(typeof (NumericComponent))]
	public static class Unit2DHelper
	{
		public static UnitInfo CreateUnitInfo(Unit2D unit)
		{
			UnitInfo unitInfo = new UnitInfo();
			NumericComponent nc = unit.GetComponent<NumericComponent>();
			unitInfo.UnitId = unit.Id;
			unitInfo.ConfigId = unit.ConfigId;
			unitInfo.Type = (int) unit.Type;
			Vector3 position = unit.Position;
			unitInfo.X = position.x;
			unitInfo.Y = position.y;
			unitInfo.Z = position.z;
			foreach ((int key, long value) in nc.NumericDic)
			{
				unitInfo.Ks.Add(key);
				unitInfo.Vs.Add(value);
			}

			return unitInfo;
		}
	}
}