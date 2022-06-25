using System;
using System.Reflection;

namespace ET
{
    [FriendOf(typeof(StateMachineDispatcherComponent))]
    public static class StateMachineDispatcherComponentSystem
    {
        [ObjectSystem]
        public class StateMachineDispatcherComponentAwakeSystem: AwakeSystem<StateMachineDispatcherComponent>
        {
            public override void Awake(StateMachineDispatcherComponent self)
            {
                StateMachineDispatcherComponent.Instance = self;
                self.Load();
            }
        }

        [ObjectSystem]
        public class StateMachineDispatcherComponentLoadSystem: LoadSystem<StateMachineDispatcherComponent>
        {
            public override void Load(StateMachineDispatcherComponent self)
            {
                self.Load();
            }
        }

        [ObjectSystem]
        public class StateMachineDispatcherComponentDestroySystem: DestroySystem<StateMachineDispatcherComponent>
        {
            public override void Destroy(StateMachineDispatcherComponent self)
            {
                self.StateHandlers.Clear();
                StateMachineDispatcherComponent.Instance = null;
            }
        }
        
        public static void Load(this StateMachineDispatcherComponent self)
        {
            self.StateHandlers.Clear();
            
            var types = Game.EventSystem.GetTypes(typeof(StateMachineAttribute));
            foreach (Type type in types)
            { 
                
                StateMachineState state = Activator.CreateInstance(type) as StateMachineState;
                if (state == null)
                {
                    Log.Error($"type is not state: {type.Name}");
                    continue;
                }
                var att = type.GetCustomAttribute(typeof(StateMachineAttribute)) as StateMachineAttribute;
                self.StateHandlers.Add(att.SelfState, state);
            }
        }
    }
}