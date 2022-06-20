using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ET.Client
{
    [FriendOf(typeof(Input2DComponent))]
    public static class Input2DComponentSystem
    {
        [ObjectSystem]
        public class Input2DComponentAwakeSystem : AwakeSystem<Input2DComponent>
        {
            public override void Awake(Input2DComponent self)
            {
                self.MyUnit2D = self.GetParent<Unit2D>();
            }
        }

        [ObjectSystem]
        public class Input2DComponentUpdateSystem : UpdateSystem<Input2DComponent>
        {
            public override void Update(Input2DComponent self)
            {
                self.Update();
            }
        }
       

        public static void Update(this Input2DComponent self)
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
                //self.Jump = true;
            }
        }
    }
}