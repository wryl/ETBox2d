using System;

namespace ET
{
	public class StateMachine2D:Entity ,IAwake
	{
		public bool TriggerEvents { get; set; }
		public CharacterConditions CurrentState { get;  set; }
		/// the character's movement state before entering the current one
		public CharacterConditions PreviousState { get;  set; }
        public delegate void OnStateChangeDelegate();
        public OnStateChangeDelegate OnStateChange;
	}

	[StateMachine(CharacterConditions.Normal)]
	public class StateMachineState_Idle:StateMachineState
	{
		public override int OnEnter(StateMachine2D stateMachine)
		{
			throw new NotImplementedException();
		}

		public override int OnExit(StateMachine2D stateMachine)
		{
			throw new NotImplementedException();
		}
	}
}