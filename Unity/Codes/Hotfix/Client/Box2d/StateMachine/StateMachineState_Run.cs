using System;
using ET.Client;
using ET.EventType;

namespace ET
{
    [StateMachine(CharacterMovementStates.Running)]
    public class StateMachineState_Run:StateMachineState
    {
        public override bool CheckBeforeEnter(StateMachine2D stateMachine)
        {
            if (stateMachine.CurrentState is 
                CharacterMovementStates.Dashing or 
                CharacterMovementStates.Jumping or 
                CharacterMovementStates.Falling
                )
            {
                return false;
            }
            return true;
        }

        public override void OnEnter(StateMachine2D stateMachine)
        {
        }

        public override void OnExit(StateMachine2D stateMachine)
        {
        }
    }
}