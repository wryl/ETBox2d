using System;

namespace ET
{

	public class StateMachine2D:Entity ,IAwake
	{
		public bool TriggerEvents { get; set; }
		/// the name of the target gameobject
		/// the current character's movement state
		public CharacterConditions CurrentState { get;  set; }
		/// the character's movement state before entering the current one
		public CharacterConditions PreviousState { get;  set; }
        public delegate void OnStateChangeDelegate();
        public OnStateChangeDelegate OnStateChange;
	}
}