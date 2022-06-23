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
            if (self.IsRunning)
            {
                return;
            }
            Game.EventSystem.Publish(self.GetParent<Unit2D>(),new EventType.UnitDashStart());
            self.Token = new ETCancellationToken();
            self.IsRunning = true;
            if (await TimerComponent.Instance.WaitAsync(200, self.Token))
            {
                self.IsRunning = false;
                Game.EventSystem.Publish(self.GetParent<Unit2D>(),new EventType.UnitDashEnd());
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