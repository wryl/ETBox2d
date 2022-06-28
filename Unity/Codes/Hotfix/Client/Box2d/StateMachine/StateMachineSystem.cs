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
                self.CurrentState = CharacterMovementStates.Idle;
            }
        }
        public static bool ChangeState(this StateMachine2D self, CharacterMovementStates newState)
        {
            if (newState.Equals(self.CurrentState))
            {
                return false;
            }
            StateMachineDispatcherComponent.Instance.StateDictionary.TryGetValue(newState, out var enterstate);
            if (enterstate == null)
            {
                Log.Error($"not found state:{newState}");
                return false;
            }
            if (!enterstate.CheckBeforeEnter(self))
            {
                return false;
            }
            Log.Debug("ChangeState"+newState.ToString());
            self.PreviousState = self.CurrentState;
            self.CurrentState = newState;
            self.OnStateChange?.Invoke();
            if (self.TriggerEvents)
            {
                // 变更动画事件
                Game.EventSystem.Publish(self.GetParent<Unit2D>(), new UnitChangeAnmi(){MovementStates = self.CurrentState});
            }
            StateMachineDispatcherComponent.Instance.StateDictionary.TryGetValue(self.PreviousState, out StateMachineState exitstate);
            exitstate.OnExit(self);
            enterstate.OnEnter(self);
            return true;
        }
    }
}