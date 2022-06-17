namespace ET
{
    [FriendOf(typeof (ET.Unit2D))]
    public static class Unit2DSystem
    {
        public class Unit2DAwakeSystem: AwakeSystem<Unit2D, int>
        {
            public override void Awake(Unit2D self, int configId)
            {
                self.ConfigId = configId;
            }
        }
    }
}