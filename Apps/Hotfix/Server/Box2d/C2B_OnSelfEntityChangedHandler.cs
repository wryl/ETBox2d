using System.Numerics;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Box2dWorld)]
    public class C2B_OnSelfEntityChangedHandler: AMActorLocationHandler<Unit2D, C2B_OnSelfEntityChanged>
    {
        protected override async ETTask Run(Unit2D unit, C2B_OnSelfEntityChanged message)
        {
            // unit.Position= new Vector3(message.X/100f, message.Y/100f, 0);
            // unit.GetComponent<StateMachine2D>().CurrentState = (CharacterMovementStates) message.CharacterStates;
            var msg = new B2C_OnEntityChanged();
            msg.CharacterCMD = message.CharacterCMD;
            msg.Id = unit.Id;
            MessageHelper.BroadcastToAllNotSelf(unit.Domain, unit.Id, msg);
            await ETTask.CompletedTask;
        }

    }
}