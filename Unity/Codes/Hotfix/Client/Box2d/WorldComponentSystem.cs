using System;
using System.Collections.Generic;
using System.Numerics;
using Box2DSharp.Collision.Collider;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace ET
{


    [FriendOf(typeof(Box2dWorldComponent))]
    public static class WorldComponentSystem
    {
        public class WorldComponentDestroySystem: DestroySystem<Box2dWorldComponent>
        {
            public override void Destroy(Box2dWorldComponent self)
            {
                self.World.Dispose();
                self.World = null;
            }
        }
        public class Box2dWorldComponentFixedUpdateSystem : FixedUpdateSystem<Box2dWorldComponent>
        {
            public override void FixedUpdate(Box2dWorldComponent self)
            {
                self.Step();
                self.World.DebugDraw();
            }

        }
        // public class Box2dWorldComponentLateUpdateSystem : LateUpdateSystem<Box2dWorldComponent>
        // {
        //     public override void LateUpdate(Box2dWorldComponent self)
        //     {
        //         
        //     }
        // }
        public class WorldComponentAwakeSystem: AwakeSystem<Box2dWorldComponent>
        {
            public override void Awake(Box2dWorldComponent self)
            {
                self.World = new World(new Vector2(0, 0));
                self.World.AllowSleep = false;
                self.World.SetContactListener(new Box2dWorldContactListener(self));
                var groundBodyDef = new BodyDef {BodyType = BodyType.StaticBody};
                groundBodyDef.Position.Set(0.0f, -1f);
                var groundBody = self.World.CreateBody(groundBodyDef);
                var groundBox = new PolygonShape();
                groundBox.SetAsBox(1000.0f, 1.0f);
                groundBody.CreateFixture(groundBox, 0.0f);
                Game.EventSystem.Publish(self, new EventType.After2DWorldCreate() {});
            }
        }
       
        public static Body CreateBoxCollider(this Box2dWorldComponent self,Body2dComponent component, float x, float y, float hx, float hy)
        {
            var bd = new BodyDef
            {
                BodyType = BodyType.DynamicBody,
                Position = new Vector2(x, y),
                AllowSleep = false
            };
            var body = self.World.CreateBody(bd);
            body.IsBullet = false;
            var shape = new PolygonShape();
            shape.SetAsBox(hx, hy, Vector2.Zero, 0);
            body.CreateFixture(shape, 1.0f);
            body.UserData = component;
            self.bodyComponents.Add(body, component);
            return body;
        }
        public static Body CreateStaticBoxCollider(this Box2dWorldComponent self,Body2dComponent component, float x, float y, float hx, float hy)
        {
            var bd = new BodyDef
            {
                BodyType = BodyType.StaticBody,
                Position = new Vector2(x, y)
            };
            var body = self.World.CreateBody(bd);
            body.IsBullet = false;
            var shape = new PolygonShape();
            shape.SetAsBox(hx, hy, Vector2.Zero, 0);
            body.CreateFixture(shape, 1.0f);
            body.UserData = component;

            self.bodyComponents.Add(body, component);
            return body;
        }
        public static void Remove(this Box2dWorldComponent self,Body body)
        {
            if (self.bodyComponents.ContainsKey(body))
            {
                self.bodyComponents.Remove(body);
                self.World.DestroyBody(body);
            }
        }
        public static void Step(this Box2dWorldComponent self)
        {
            self.World.Step(self._dt, 8, 3);
        }
      

    }
}