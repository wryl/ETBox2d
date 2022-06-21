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
                self.DirectionLeft = true;
            }
        }

        [FriendOfAttribute(typeof(ET.CharacterhorizontalMoveComponent))]
        [FriendOfAttribute(typeof(ET.CharacterGravityComponent))]
        public class Controller2DComponentUpdateSystem : UpdateSystem<Controller2DComponent>
        {
            public override void Update(Controller2DComponent self)
            {
                Vector2 dir = Vector2.Zero;
                dir.X += self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed;
                dir.X += self.MyUnit2D.GetComponent<CharacterDashComponent>().GetValue();
                if (dir.X > 0)
                {
                    self.DirectionLeft = false;
                    Game.EventSystem.Publish(self.MyUnit2D, new EventType.CharacterChangeFace() { FaceRight = self.DirectionLeft });
                }
                else if (dir.X < 0)
                {
                    self.DirectionLeft = true;
                    Game.EventSystem.Publish(self.MyUnit2D, new EventType.CharacterChangeFace() { FaceRight = self.DirectionLeft });
                }

                dir.Y = self.MyUnit2D.GetComponent<CharacterGravityComponent>().speed+self.MyUnit2D.GetComponent<CharacterJumpComponent>().GetValue();
                self.MyUnit2D.GetComponent<Body2dComponent>().Body.SetLinearVelocity(dir);
            }
        }


    }
}