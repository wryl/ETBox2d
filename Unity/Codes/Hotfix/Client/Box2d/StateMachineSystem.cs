using ET.EventType;

namespace ET
{
    [FriendOf(typeof (StateMachine2D))]
    public static class StateMachine2DSystem
    {
        public class DashMoveComponentAwakeSystem: AwakeSystem<StateMachine2D>
        {
            public override void Awake(StateMachine2D self)
            {
                self.TriggerEvents = true;
            }
        }
        public static void ChangeState(this StateMachine2D self,CharacterConditions newState)
        {
            if (newState.Equals(self.CurrentState))
            {
                return;
            }
            self.PreviousState = self.CurrentState;
            self.CurrentState = newState;
            self.OnStateChange?.Invoke();
            if (self.TriggerEvents)
            {
                Game.EventSystem.Publish(self,new StateChangeEvent());
            }
        }

        public static void RestorePreviousState(this StateMachine2D self)
        {
            self.CurrentState = self.PreviousState;
            self.OnStateChange?.Invoke();
            if (self.TriggerEvents)
            {
                Game.EventSystem.Publish(self,new StateChangeEvent());
            }
        }	
    }
}