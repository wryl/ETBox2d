namespace ET
{
    [FriendOf(typeof (ET.DashMoveComponent))]
    public static class DashMoveComponentSystem
    {
        public class DashMoveComponentAwakeSystem: AwakeSystem<DashMoveComponent>
        {
            public override void Awake(DashMoveComponent self)
            {
                self.speed = 10;
                self.IsDashing = false;
            }
        }
        public static async ETTask StartDash(this DashMoveComponent self)
        {
            if (!self.IsDashing)
            {
                Game.EventSystem.Publish(self.GetParent<Unit2D>(),new EventType.UnitDashStart());
                self.Token = new ETCancellationToken();
                self.IsDashing = true;
                if (await TimerComponent.Instance.WaitAsync(100, self.Token))
                {
                    self.IsDashing = false;
                    Game.EventSystem.Publish(self.GetParent<Unit2D>(),new EventType.UnitDashEnd());
                }
            }
        }
    }
}