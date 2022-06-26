using System;
using ET.Client;
using ET.EventType;

namespace ET
{
    [StateMachine(CharacterMovementStates.Idle)]
    public class StateMachineState_Idle:StateMachineState
    {
        public override bool CheckBeforeEnter(StateMachine2D stateMachine)
        {
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