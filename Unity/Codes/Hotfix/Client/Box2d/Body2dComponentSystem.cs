using System;
using System.Numerics;
using Box2DSharp.Dynamics.Contacts;

namespace ET
{
    [FriendOf(typeof(ET.Body2dComponent))]
    public static class Body2dComponentSystem
    {
        public class Body2dComponentAwakeSystem : AwakeSystem<Body2dComponent>
        {
            public override void Awake(Body2dComponent self)
            {
                self.ParentUnit = self.GetParent<Unit2D>();
            }
        }
        public class Body2dComponentDestroySystem : DestroySystem<Body2dComponent>
        {
            public override void Destroy(Body2dComponent self)
            {
                self.Domain.GetComponent<Box2dWorldComponent>().Remove(self.Body);
            }
        }
        public class Body2dComponentUpdateSystem : UpdateSystem<Body2dComponent>
        {
            public override void Update(Body2dComponent self)
            {
                self.Update();
            }
        }
        public static void Update(this Body2dComponent self)
        {
            if (self.IsBeForce)
            {
                var position = self.Body.GetPosition();
                if (Math.Abs(position.X - self.ParentUnit.Position.X) > 0.001f || Math.Abs(position.Y - self.ParentUnit.Position.Y) > 0.001f)
                {
                    self.ParentUnit.LastPosition = self.ParentUnit.Position;
                    self.ParentUnit.Position = new Vector3(position.X, position.Y,0);
                }
                self.Angle = self.Body.GetAngle();
            }
            else
            {
                self.Body.SetTransform(new System.Numerics.Vector2(self.ParentUnit.Position.X, self.ParentUnit.Position.Y), self.Angle);
            }
        }
        public static Body2dComponent CreateBody(this Body2dComponent self, float hx, float hy)
        {
            self.Body = self.Domain.GetComponent<Box2dWorldComponent>().CreateBoxCollider(self, self.ParentUnit.Position.Y, self.ParentUnit.Position.Y, hx, hy);
            return self;
        }
    }
}