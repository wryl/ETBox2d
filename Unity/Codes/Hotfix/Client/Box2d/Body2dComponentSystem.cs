using System;
using Box2DSharp.Dynamics.Contacts;
using UnityEngine;

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
                if (Math.Abs(position.X - self.ParentUnit.Position.x) > 0.0001f || Math.Abs(position.Y - self.ParentUnit.Position.y) > 0.0001f)
                {
                    self.ParentUnit.Position = new Vector2(position.X, position.Y);
                }
                self.Angle = self.Body.GetAngle();
            }
            else
            {
                self.Body.SetTransform(new System.Numerics.Vector2(self.ParentUnit.Position.x, self.ParentUnit.Position.y), self.Angle);
            }
        }
        public static Body2dComponent CreateBody(this Body2dComponent self, float hx, float hy)
        {
            self.Body = self.Domain.GetComponent<Box2dWorldComponent>().CreateBoxCollider(self, self.ParentUnit.Position.x, self.ParentUnit.Position.y, hx, hy);
            return self;
        }
    }
}