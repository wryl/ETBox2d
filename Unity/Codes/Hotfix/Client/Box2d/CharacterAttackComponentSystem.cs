using System;

namespace ET
{
    [FriendOf(typeof (ET.CharacterAttackComponent))]
    public static class CharacterAttackComponentSystem
    {
        public class CharacterAttackComponentAwakeSystem: AwakeSystem<CharacterAttackComponent>
        {
            public override void Awake(CharacterAttackComponent self)
            {
                self.IsRunning = false;
                self.CurrentIndexAtack = 0;
                self.AddChildWithId<CharacterAttackPartComponent,int,int>(0,800,10);
                self.AddChildWithId<CharacterAttackPartComponent,int,int>(1,800,20);
                self.AddChildWithId<CharacterAttackPartComponent,int,int>(2,800,30);
            }
        }
        public static async ETTask StartAtack(this CharacterAttackComponent self)
        {
            int attackpartid = self.CurrentIndexAtack % self.Children.Count;
            self.CurrentIndexAtack++;
            self.Token = new ETCancellationToken();
            self.IsRunning = true;
            var attackpart=self.GetChild<CharacterAttackPartComponent>(attackpartid);
            Log.Debug("CharacterAttackComponent attackpart"+attackpartid.ToString());
            if (await TimerComponent.Instance.WaitAsync(attackpart.Duratuion, self.Token))
            {
                self.CurrentIndexAtack = 0;
                Log.Debug("CharacterAttackComponent CurrentIndexAtack set 0");

            }
        }
    }
}