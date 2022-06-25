using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class StateMachineDispatcherComponent: Entity, IAwake, IDestroy, ILoad
    {
        public static StateMachineDispatcherComponent Instance;
        
        public Dictionary<CharacterConditions, StateMachineState> StateHandlers = new Dictionary<CharacterConditions, StateMachineState>();
    }
}