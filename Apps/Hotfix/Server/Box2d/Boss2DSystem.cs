namespace ET
{
    public static class Boss2DSystem
    {
        public class Boss2DAwakeSystem : AwakeSystem<Boss2D>
        {
            public override void Awake(Boss2D self)
            {
                var body2d=self.AddComponent<Body2dComponent>();
                body2d.IsBeForce = true;
                body2d.CreateBody(1, 1);
                body2d.OnBeginContactAction += self.OnBeginContact;
            }
        }
        public static void OnBeginContact(this Boss2D self, Body2dComponent other)
        {
            try
            {
                Log.Debug("Boss2D OnBeginContact");
            }
            catch (System.Exception e)
            {
                Log.Error(e);
            }
        }
    }
}