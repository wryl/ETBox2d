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
                if (self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>()!=null)
                {
                    dir.X += self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed;
                }

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
                
                if (!self.IsGround)
                {
                    self.MyUnit2D.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Falling);

                }
                if (self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>()?.IsRunning==true)
                {
                    self.MyUnit2D.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Running);
                }
                else
                {
                    if (self.MyUnit2D.GetComponent<StateMachine2D>().CurrentState==CharacterMovementStates.Running)
                    {
                        self.MyUnit2D.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Idle);
                    }
                }
                switch (self.MyUnit2D.GetComponent<StateMachine2D>().CurrentState)
                {
                    case CharacterMovementStates.Dashing:
                        if (self.MyUnit2D.GetComponent<CharacterDashComponent>()!=null)
                        {
                            if (self.DirectionLeft)
                            {
                                dir.X -= self.MyUnit2D.GetComponent<CharacterDashComponent>().GetValue();
                            }
                            else
                            {
                                dir.X += self.MyUnit2D.GetComponent<CharacterDashComponent>().GetValue();
                            }
                        }
                        break;
                    case CharacterMovementStates.Jumping:
                        dir.Y += self.MyUnit2D.GetComponent<CharacterJumpComponent>().GetValue();
                        break;
                    case CharacterMovementStates.Falling:
                        if (self.MyUnit2D.GetComponent<CharacterGravityComponent>()!=null)
                        {
                            dir.Y += self.MyUnit2D.GetComponent<CharacterGravityComponent>().speed;
                        }
                        break;
                }

                self.MyUnit2D.GetComponent<Body2dComponent>().Body.SetLinearVelocity(dir);
            }
        }


    }
}