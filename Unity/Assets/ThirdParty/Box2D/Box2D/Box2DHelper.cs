using Box2DSharp.Dynamics;

namespace ET
{
    public static class Box2DHelper
    {
        public static void ChangeGravityScale(Body body,float target)
        {
            body.GravityScale = target;
        }
    }
}