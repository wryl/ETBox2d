namespace ET
{	
    public class StateMachineAttribute: BaseAttribute
    {
        public CharacterConditions SelfState;
        public StateMachineAttribute(CharacterConditions conditions)
        {
            this.SelfState = conditions;
        }
    }
    public abstract class StateMachineState
    {
        public CharacterConditions ConflictState;
        /// <summary>
        ///  进入新的状态
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <returns></returns>
        public abstract int OnEnter(StateMachine2D stateMachine);
        /// <summary>
        /// 退出状态执行
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <returns></returns>
        public abstract int OnExit(StateMachine2D stateMachine);

    }
}
