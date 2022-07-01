using System;
using Vector2 = System.Numerics.Vector2;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Box2dWorld)]
    public class C2B_AddForceHandler: AMActorLocationRpcHandler<Unit2D, C2B_AddForce, B2C_AddForce>
    {
        protected override async ETTask Run(Unit2D unit, C2B_AddForce request, B2C_AddForce response, Action reply)
        {
            await ETTask.CompletedTask;
            Unit2DComponent unitComponent = unit.Domain.GetComponent<Unit2DComponent>();
            foreach (Unit2D entity in unitComponent.Children.Values)
            {
                if (entity.GetComponent<Boss2D>()!=null)
                {
                    entity.GetComponent<CharacterJumpComponent>().StartJumpStore().Coroutine();
                }
            }
            reply();
        }
    }
}