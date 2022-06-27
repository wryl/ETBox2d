namespace ET
{
    [FriendOf(typeof (ET.CharacterDashComponent))]
    public static class DashMoveComponentSystem
    {
        public class DashMoveComponentAwakeSystem: AwakeSystem<CharacterDashComponent>
        {
            public override void Awake(CharacterDashComponent self)
            {
                self.speed = 5;
                self.IsRunning = false;
            }
        }
        public static async ETTask StartDash(this CharacterDashComponent self)
        {

            if (!self.Parent.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Dashing))
            {
                return;
            }

            self.Token = new ETCancellationToken();
            self.IsRunning = true;
            if (await TimerComponent.Instance.WaitAsync(200, self.Token))
            {
                self.Parent.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Idle);
                self.IsRunning = false;
            }
        }

        public static float GetValue(this CharacterDashComponent self)
        {
            if (self.IsRunning)
            {
                return self.speed ;
            }

            return 0;
        }
    }
}