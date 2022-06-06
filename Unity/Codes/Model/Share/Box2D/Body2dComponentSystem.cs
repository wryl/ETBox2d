using Box2DSharp.Dynamics.Contacts;
using UnityEngine;

namespace ET
{
    public static class  Body2dComponentSystem
    {
        public class Body2dComponentAwakeSystem : AwakeSystem<Body2dComponent>
        {
            public override void Awake(Body2dComponent self)
            {
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
            if (self.Parent.GetComponent<TransformComponent>() == null)
            {
                return;
            }
            if (self.Position != self.lastPosition)
            {
                //Log.Debug("Position = " + Position.ToString());
                self.lastPosition = self.Position;
            }

            if (!self.IsBeForce)
            {
                self.Body.SetTransform(new System.Numerics.Vector2(self.Position.x, self.Position.y), self.Angle);
            }
            else
            {
                var position = self.Body.GetPosition();
                self.Position = new Vector2(position.X, position.Y);
                self.Angle = self.Body.GetAngle();
            }
        }
        public static Body2dComponent CreateBody(this Body2dComponent self,float hx, float hy)
        {
            self.Body = self.Domain.GetComponent<Box2dWorldComponent>().CreateBoxCollider(self, self.Position.x, self.Position.y, hx, hy);
            return self;
        }
        public static void BeginContact(this Body2dComponent self,Contact contact, Body2dComponent other)
        {
            Log.Debug($"Body2dComponent BeginContact");
            try
            {
                self.OnBeginContactAction?.Invoke(other);
            }
            catch (System.Exception e)
            {
                Log.Error(e);
            }
        }

        public static void EndContact(this Body2dComponent self,Contact contact)
        {
        }
    }
}