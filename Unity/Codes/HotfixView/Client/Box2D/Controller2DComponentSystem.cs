using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ET.Client
{
    [FriendClass(typeof(Controller2DComponent))]
    public static class Controller2DComponentSystem
    {
        [ObjectSystem]
        public class Controller2DComponentAwakeSystem : AwakeSystem<Controller2DComponent>
        {
            public override void Awake(Controller2DComponent self)
            {
                self.X = 0;
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
        
        public static void Update(this Controller2DComponent self)
        {
            GameObject gameObject = self.Parent.GetComponent<GameObjectComponent>().GameObject;
            if (Input.GetKey(KeyCode.A))
            {
                self.Parent.GetComponent<Body2dComponent>().Body.SetLinearVelocity(new Vector2(-1,self.Parent.GetComponent<Body2dComponent>().Body.LinearVelocity.Y));
                gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(1);
                gameObject.transform.localScale = new Vector3(1,1,1);
            }
            else if (Input.GetKey(KeyCode.D)) {
                self.Parent.GetComponent<Body2dComponent>().Body.SetLinearVelocity(new Vector2(1,self.Parent.GetComponent<Body2dComponent>().Body.LinearVelocity.Y));
                gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(1);
                gameObject.transform.localScale = new Vector3(-1,1,1);

            }
            else
            {
                
                self.Parent.GetComponent<Body2dComponent>().Body.SetLinearVelocity(new Vector2(0,self.Parent.GetComponent<Body2dComponent>().Body.LinearVelocity.Y));
                gameObject.GetComponent<SPUM_Prefabs>().PlayAnimation(0);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                self.Dash = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                self.Jump = true;
            }
        }
    }
}