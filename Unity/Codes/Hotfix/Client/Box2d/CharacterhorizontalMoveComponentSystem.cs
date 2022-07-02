using System;

namespace ET
{
    [FriendOf(typeof (ET.CharacterhorizontalMoveComponent))]
    public static class CharacterhorizontalMoveComponentSystem
    {
        public class CharacterhorizontalMoveComponentAwakeSystem: AwakeSystem<CharacterhorizontalMoveComponent>
        {
            public override void Awake(CharacterhorizontalMoveComponent self)
            {
                self.IsRunning = false;
                self.speed = 3;
            }
        }
        public static float GetValue(this CharacterhorizontalMoveComponent self)
        {
            if (self.Left&&self.Right)
            {
                self.IsRunning = false;
                return 0;
            }
            else if(self.Left)
            {
                self.IsRunning = true;
                self.CurrFaceLeft = true;
                return -3f;
            }
            else if(self.Right)
            {
                self.IsRunning = true;
                self.CurrFaceLeft = false;
                return 3f;
            }
            else
            {
                self.IsRunning = false;
                return 0;
            }
        }
    }
}