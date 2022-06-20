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

        public static void Update(this Controller2DComponent self)
        {
          
        }
    }
}