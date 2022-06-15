using System;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Box2dWorld)]
    public class M2M_Unit2DTransferRequestHandler: AMActorRpcHandler<Scene, M2M_Unit2DTransferRequest, M2M_Unit2DTransferResponse>
    {
        protected override async ETTask Run(Scene scene, M2M_Unit2DTransferRequest request, M2M_Unit2DTransferResponse response, Action reply)
        {
            await ETTask.CompletedTask;
            Unit2DComponent unitComponent = scene.GetComponent<Unit2DComponent>();
            Unit2D unit = request.Unit;
            unitComponent.AddChild(unit);
            foreach (Entity entity in request.Entitys)
            {
                unit.AddComponent(entity);
            }
            unit.AddComponent<MailBoxComponent>();
            unit.AddComponent<Player2D>();
            // 通知客户端创建My Unit
            M2C_CreateMyUnit2D m2CCreateUnits = new M2C_CreateMyUnit2D();
            m2CCreateUnits.Unit = Server.Unit2DHelper.CreateUnitInfo(unit);
            MessageHelper.SendToClient(unit, m2CCreateUnits);
            M2C_CreateUnit2Ds createUnits = new M2C_CreateUnit2Ds();
            foreach (Unit2D otherunit in unitComponent.Children.Values)
            {
                if (otherunit.GetComponent<Boss2D>()!=null)
                {
                    createUnits.Units.Add(Server.Unit2DHelper.CreateUnitInfo(otherunit));
                }
            }
            MessageHelper.SendToClient(unit, createUnits);
            response.NewInstanceId = unit.InstanceId;
            reply();
        }

		
    }
}