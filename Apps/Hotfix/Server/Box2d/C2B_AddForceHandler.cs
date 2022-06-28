using System;
using System.Numerics;
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
                    //entity.GetComponent<Body2dComponent>().Body.ApplyForce(new Vector2(0,200),new Vector2(0,0),true);
                    //entity.GetComponent<Body2dComponent>().Body.ApplyLinearImpulseToCenter(new Vector2(0,1),true);
                }
            }
            reply();
        }
    }
    [ActorMessageHandler(SceneType.Box2dWorld)]
    public class C2B_OnSelfEntityChangedHandler: AMActorLocationHandler<Unit2D, C2B_OnSelfEntityChanged>
    {
        protected override async ETTask Run(Unit2D unit, C2B_OnSelfEntityChanged message)
        {
            unit.Position= new Vector3(message.X/100f, message.Y/100f, 0);       
            await ETTask.CompletedTask;
        }

    }
}