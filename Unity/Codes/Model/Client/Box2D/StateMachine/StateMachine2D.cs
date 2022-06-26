using System;

namespace ET
{
	[ComponentOf(typeof (Unit2D))]
	public class StateMachine2D:Entity ,IAwake
	{
		public bool TriggerEvents { get; set; }
		public CharacterMovementStates CurrentState { get;  set; }
		public CharacterMovementStates PreviousState { get;  set; }
        public delegate void OnStateChangeDelegate();
        public OnStateChangeDelegate OnStateChange;
	}


}