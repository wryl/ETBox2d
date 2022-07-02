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

        // public class Controller2DComponentUpdate: UpdateSystem<Controller2DComponent>
        // {
        //     public override void Update(Controller2DComponent self)
        //     {
        //         
        //     }
        // }

        public class Controller2DComponentFixedUpdate : FixedUpdateSystem<Controller2DComponent>
        {
            public override void FixedUpdate(Controller2DComponent self)
            { 
                self.PreCmdSet();
                self.CmdSet();
                self.ControllerMove();
                self.SyncCmd();
                self.CleanAfterUpdate();
            }
        }
        /// <summary>
        /// 预处理,如果有命令收集组件说明接受外部的控制
        /// </summary>
        /// <param name="self"></param>
        public static void PreCmdSet(this Controller2DComponent self)
        {
            var cmdComponent = self.MyUnit2D.GetComponent<CmdCollectorComponent>();
            if (cmdComponent!=null)
            {
                self.CurrCmdType = cmdComponent.GetCmd();
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
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().Left=true;
            }
            if ((self.CurrCmdType&CmdType.D)==CmdType.D)
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().Right=true;
            }
            if ((self.CurrCmdType&CmdType.AUp)==CmdType.AUp)
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().Left=false;
            }
            if ((self.CurrCmdType&CmdType.DUp)==CmdType.DUp)
            {
                self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().Right=false;
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

        public static void ControllerMove(this Controller2DComponent self)
        {
             Vector2 dir = Vector2.Zero;
            if (self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>()!=null)
            {
                dir.X += self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().GetValue();
            }

            if (self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().CurrFaceLeft!=self.DirectionLeft )
            {
                self.DirectionLeft = self.MyUnit2D.GetComponent<CharacterhorizontalMoveComponent>().CurrFaceLeft;
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

            self.MyUnit2D.GetComponent<Body2dComponent>()?.Body.SetLinearVelocity(dir);
        }
        /// <summary>
        /// 网络同步命令,上一帧不是idle的话.同步一帧idle过去.让最后状态一致
        /// </summary>
        /// <param name="self"></param>
        public static void SyncCmd(this Controller2DComponent self)
        {
            if (self.CurrCmdType!=CmdType.Idle||self.LastCmdType!=CmdType.Idle)
            {
                self.MyUnit2D.GetComponent<EntitySyncComponent>()?.SyncToServer((int)self.CurrCmdType);
                self.LastCmdType = self.CurrCmdType;
            }
        }

        public static void CleanAfterUpdate(this Controller2DComponent self)
        {
            self.CurrCmdType = CmdType.Idle;
        }
    }
}