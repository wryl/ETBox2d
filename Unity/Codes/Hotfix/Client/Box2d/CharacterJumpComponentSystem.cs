using System;
using UnityEngine;

namespace ET
{
    [FriendOf(typeof (ET.CharacterJumpComponent))]
    public static class CharacterJumpComponentSystem
    {
        public class CharacterJumpComponentAwakeSystem: AwakeSystem<CharacterJumpComponent>
        {
            public override void Awake(CharacterJumpComponent self)
            {
                self.BaseSpeed = 10;
                self.MinJumpHold = 50;
                self.MaxJumpHold = 240;
                self.JumpNum = 1;
            }
        }
        public class CharacterJumpComponentUpdateSystem: UpdateSystem<CharacterJumpComponent>
        {
            public override void Update(CharacterJumpComponent self)
            {
                //self.Domain.GetComponent<Box2dWorldComponent>().World.RayCast()
            }
        }
        public static async ETTask StartJumpStore(this CharacterJumpComponent self)
        {
            if (self.JumpNum<=0)
            {
                return;
            }
            if (self.IsRunning)
            {
                return;
            }
            if (!self.Parent.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Jumping))
            {
                return;
            }
            self.Token = new ETCancellationToken();
            self.IsRunning = true;
            self.JumpNum--;
            self.StartTime = TimeHelper.ClientNow();
            if (await TimerComponent.Instance.WaitAsync(self.MaxJumpHold, self.Token))
            {
                self.EndJump().Coroutine();
            }
        }

        private static async ETTask EndJump(this CharacterJumpComponent self)
        {
            self.EndTime = TimeHelper.ClientNow();
            self.IsStoring = false;
            var gotime = Math.Max(self.MinJumpHold,self.EndTime - self.StartTime);
            if (gotime<self.MinJumpHold)
            {
                await TimerComponent.Instance.WaitAsync(self.MinJumpHold);
                self.Parent.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Idle);
                self.IsRunning = false;
            }
            else
            {
                self.Parent.GetComponent<StateMachine2D>().ChangeState(CharacterMovementStates.Idle);
                self.IsRunning = false;
            }
        }

        public static void EndJumpStore(this CharacterJumpComponent self)
        {
            if (!self.IsRunning)
            {
                return;
            }

            if ( TimeHelper.ClientNow()>self.StartTime+self.MaxJumpHold)
            {
                return;
            }
            self.Token?.Cancel();
            self.EndJump().Coroutine();
        }
        public static void RestoreJumpNum(this CharacterJumpComponent self)
        {
            self.JumpNum = 1;
        }
        public static float GetValue(this CharacterJumpComponent self)
        {
            if (self.IsRunning)
            {
                //Mathf.Clamp(TimeHelper.ClientNow()-self.StartTime),self.MinJumpHold,self.MaxJumpHold)
                return self.BaseSpeed * Math.Min( Math.Max(self.MinJumpHold,(TimeHelper.ClientNow()-self.StartTime)),self.MaxJumpHold) / 100f;
            }
            return 0;
        }
    }
}