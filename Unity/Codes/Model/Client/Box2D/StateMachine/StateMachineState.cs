namespace ET
{	
    public class StateMachineAttribute: BaseAttribute
    {
        public CharacterMovementStates SelfState;
        public StateMachineAttribute(CharacterMovementStates conditions)
        {
            this.SelfState = conditions;
        }
    }
    public abstract class StateMachineState
    {
        /// <summary>
        ///  进入前的检测
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <returns></returns>
        public abstract bool CheckBeforeEnter(StateMachine2D stateMachine);
        /// <summary>
        ///  进入新的状态
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <returns></returns>
        public abstract void OnEnter(StateMachine2D stateMachine);
        /// <summary>
        /// 退出状态执行
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <returns></returns>
        public abstract void OnExit(StateMachine2D stateMachine);

    }
}
