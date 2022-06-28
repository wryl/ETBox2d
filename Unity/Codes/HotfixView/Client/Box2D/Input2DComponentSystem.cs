using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ET.Client
{
    [FriendOf(typeof(Input2DComponent))]
    [FriendOfAttribute(typeof(CharacterhorizontalMoveComponent))]
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
            GameObject gameObject = self.Parent.GetComponent<GameObjectComponent>().GameObject;
            if (Input.GetKey(KeyCode.A))
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed = -3;
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().IsRunning = true;
                // self.Parent.GetComponent<Body2dComponent>().Body.SetLinearVelocity(new Vector2(-1,self.Parent.GetComponent<Body2dComponent>().Body.LinearVelocity.Y));
                //  gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(1);
                //  gameObject.transform.localScale = new Vector3(1,1,1);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed = 3;
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().IsRunning = true;

                // self.Parent.GetComponent<Body2dComponent>().Body.SetLinearVelocity(new Vector2(1,self.Parent.GetComponent<Body2dComponent>().Body.LinearVelocity.Y));
                // gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(1);
                // gameObject.transform.localScale = new Vector3(-1,1,1);

            }
            else
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed = 0;
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().IsRunning = false;

            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                self.MyUnit2D.GetComponent<CharacterDashComponent>().StartDash().Coroutine();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                self.MyUnit2D.GetComponent<CharacterJumpComponent>().StartJumpStore().Coroutine();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                self.MyUnit2D.GetComponent<CharacterJumpComponent>().EndJumpStore();
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                self.MyUnit2D.GetComponent<CharacterAttackComponent>().StartAtack().Coroutine();
            }
        }
    }
}