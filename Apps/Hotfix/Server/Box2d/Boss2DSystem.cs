namespace ET
{
    public static class Boss2DSystem
    {
        public class Boss2DAwakeSystem : AwakeSystem<Boss2D>
        {
            public override void Awake(Boss2D self)
            {

            }
        }
        public static void OnBeginContact(this Boss2D self, Body2dComponent selfbody,Body2dComponent other)
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
    public static class Player2DSystem
    {
        public class Player2DAwakeSystem : AwakeSystem<Player2D>
        {
            public override void Awake(Player2D self)
            {

            }
        }
        public static void OnBeginContact(this Player2D self, Body2dComponent other)
        {
            try
            {
                Log.Debug("Player2D OnBeginContact");
            }
            catch (System.Exception e)
            {
                Log.Error(e);
            }
        }
    }
}