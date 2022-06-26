using ET.EventType;

namespace ET
{
    [FriendOf(typeof(StateMachine2D))]
    [FriendOf(typeof(ET.StateMachineDispatcherComponent))]
    public static class StateMachine2DSystem
    {
        public class DashMoveComponentAwakeSystem : AwakeSystem<StateMachine2D>
        {
            public override void Awake(StateMachine2D self)
            {
                self.TriggerEvents = true;
            }
        }
        public static void ChangeState(this StateMachine2D self, CharacterMovementStates newState)
        {
            if (newState.Equals(self.CurrentState))
            {
                return;
            }
            StateMachineDispatcherComponent.Instance.StateDictionary.TryGetValue(newState, out var enterstate);
            if (enterstate == null)
            {
                Log.Error($"not found state:{self.CurrentState}");
                return;
            }
            if (!enterstate.CheckBeforeEnter(self))
            {
                return;
            }
            self.PreviousState = self.CurrentState;
            self.CurrentState = newState;
            self.OnStateChange?.Invoke();
            if (self.TriggerEvents)
            {
                // 变更动画事件
                Game.EventSystem.Publish(self, new UnitChangeAnmi(){MovementStates = self.CurrentState});
            }
            StateMachineDispatcherComponent.Instance.StateDictionary.TryGetValue(self.PreviousState, out StateMachineState exitstate);
            exitstate.OnExit(self);
            enterstate.OnEnter(self);
        }
    }
}