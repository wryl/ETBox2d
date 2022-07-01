using System;
using System.Numerics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace ET.Client
{
    [FriendOf(typeof(Controller2DComponent))]    
    [FriendOf(typeof(CharacterhorizontalMoveComponent))]

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

        public class Controller2DComponentUpdate: UpdateSystem<Controller2DComponent>
        {
            public override void Update(Controller2DComponent self)
            {
                self.CmdSet();
            }
        }

        [FriendOfAttribute(typeof(ET.CharacterhorizontalMoveComponent))]
        [FriendOfAttribute(typeof(ET.CharacterGravityComponent))]
        public class Controller2DComponentFixedUpdate : FixedUpdateSystem<Controller2DComponent>
        {
            public override void FixedUpdate(Controller2DComponent self)
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
        /// <summary>
        /// 根据操作调整各项组件
        /// </summary>
        /// <param name="self"></param>
        public static void CmdSet(this Controller2DComponent self)
        {
            if ((self.CurrCmdType&CmdType.A)==CmdType.A)
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed = -3;
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().IsRunning = true;
            }
            else if ((self.CurrCmdType&CmdType.D)==CmdType.D)
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed = 3;
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().IsRunning = true;
            }
            else
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().speed = 0;
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().IsRunning = false;
            }
            if ((self.CurrCmdType&CmdType.LeftShift)==CmdType.LeftShift)
            {
                self.MyUnit2D.GetComponent<CharacterDashComponent>().StartDash().Coroutine();
            }
            if ((self.CurrCmdType&CmdType.Space)==CmdType.Space)
            {
                self.MyUnit2D.GetComponent<CharacterJumpComponent>().StartJumpStore().Coroutine();
            }
            if ((self.CurrCmdType&CmdType.SpaceUp)==CmdType.SpaceUp)
            {
                self.MyUnit2D.GetComponent<CharacterJumpComponent>().EndJumpStore();
            }
            if ((self.CurrCmdType&CmdType.J)==CmdType.J)
            {
                self.MyUnit2D.GetComponent<CharacterAttackComponent>().StartAtack().Coroutine();
            }
        }
    }
}