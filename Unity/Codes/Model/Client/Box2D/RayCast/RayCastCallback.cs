using System.Numerics;
using Box2DSharp.Dynamics;

namespace ET
{
    public class RayCastClosestGroundCallback : IRayCastCallback
    {
        public bool Hit;

        public Vector2 Normal;

        public Vector2 Point;

        public RayCastClosestGroundCallback()
        {
            Hit = false;
        }

        public float RayCastCallback(Fixture fixture, in Vector2 point, in Vector2 normal, float fraction)
        {
            var body = fixture.Body;
            if (body.BodyType!=BodyType.StaticBody)
            {
                return -1.0f;
            }
            Hit = true;
            Point = point;
            Normal = normal;
            // By returning the current fraction, we instruct the calling code to clip the ray and
            // continue the ray-cast to the next fixture. WARNING: do not assume that fixtures
            // are reported in order. However, by clipping, we can always get the closest fixture.
            return fraction;
        }
    }
}