using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ET.Client
{
    [FriendOf(typeof(Controller2DComponent))]
    public static class Controller2DComponentSystem
    {
        [ObjectSystem]
        public class Controller2DComponentAwakeSystem : AwakeSystem<Controller2DComponent>
        {
            public override void Awake(Controller2DComponent self)
            {
                self.Velocity = Vector2.Zero;
                self.laterDirection = new Vector2(1,0);
            }
        }

        [ObjectSystem]
        public class Controller2DComponentUpdateSystem : UpdateSystem<Controller2DComponent>
        {
            public override void Update(Controller2DComponent self)
            {
                self.Update();
            }
        }
        [ObjectSystem]
        public class Controller2DComponentFixedUpdateSystem : FixedUpdateSystem<Controller2DComponent>
        {
            public override void FixedUpdate(Controller2DComponent self)
            {
                self.FixedUpdate();
            }
        }

        public static void FixedUpdate(this Controller2DComponent self)
        {
            
        }

        public static void Update(this Controller2DComponent self)
        {
            var dir = Vector2.Zero;
            GameObject gameObject = self.Parent.GetComponent<GameObjectComponent>().GameObject;
            if (Input.GetKey(KeyCode.A))
            {
                dir.X = -1;
                // self.Parent.GetComponent<Body2dComponent>().Body.SetLinearVelocity(new Vector2(-1,self.Parent.GetComponent<Body2dComponent>().Body.LinearVelocity.Y));
                //  gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(1);
                //  gameObject.transform.localScale = new Vector3(1,1,1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.X = 1;
                // self.Parent.GetComponent<Body2dComponent>().Body.SetLinearVelocity(new Vector2(1,self.Parent.GetComponent<Body2dComponent>().Body.LinearVelocity.Y));
                // gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(1);
                // gameObject.transform.localScale = new Vector3(-1,1,1);

            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                self.MyUnit2D.GetComponent<DashMoveComponent>().StartDash().Coroutine();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                self.Jump = true;
            }
        }
    }
}