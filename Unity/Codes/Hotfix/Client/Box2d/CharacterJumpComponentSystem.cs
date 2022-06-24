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
            }
        }
        public static async ETTask StartJumpStore(this CharacterJumpComponent self)
        {
            if (self.IsRunning)
            {
                return;
            }
            //Game.EventSystem.Publish(self.GetParent<Unit2D>(),new EventType.UnitDashStart());
            self.Token = new ETCancellationToken();
            self.IsRunning = true;
            self.StartTime = TimeHelper.ClientNow();
            if (await TimerComponent.Instance.WaitAsync(self.MaxJumpHold, self.Token))
            {
                self.EndJump().Coroutine();
                //Game.EventSystem.Publish(self.GetParent<Unit2D>(),new EventType.UnitDashEnd());
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
                self.IsRunning = false;
            }
            else
            {
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
            //Game.EventSystem.Publish(self.GetParent<Unit2D>(),new EventType.UnitDashStart());
            self.Token?.Cancel();
            self.EndJump().Coroutine();
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