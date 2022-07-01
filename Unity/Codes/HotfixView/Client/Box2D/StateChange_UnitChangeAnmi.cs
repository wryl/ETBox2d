using System;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class StateChange_UnitChangeAnmi: AEvent<Unit2D, EventType.UnitChangeAnmi>
    {
        protected override async ETTask Run(Unit2D unit, EventType.UnitChangeAnmi args)
        {

            var gameObject = unit.GetComponent<GameObjectComponent>().GameObject;
            switch (args.MovementStates)
            {
                case CharacterMovementStates.Null:
                    break;
                case CharacterMovementStates.Idle:
                    gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(0);
                    break;
                case CharacterMovementStates.Walking:
                    break;
                case CharacterMovementStates.Falling:
                    break;
                case CharacterMovementStates.Running:
                    gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(1);
                    break;
                case CharacterMovementStates.Dashing:
                    gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(4);
                    break;
                case CharacterMovementStates.Diving:
                    break;
                case CharacterMovementStates.Jumping:
                    gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(5);

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            await ETTask.CompletedTask;
        }
    }
}