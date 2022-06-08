using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendClass(typeof(UILobbyComponent))]
    public static class UILobbyComponentSystem
    {
        [ObjectSystem]
        public class UILobbyComponentAwakeSystem: AwakeSystem<UILobbyComponent>
        {
            public override void Awake(UILobbyComponent self)
            {
                ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

                self.enterMap = rc.Get<GameObject>("EnterMap");
                self.enterMap.GetComponent<Button>().onClick.AddListener(() => { self.EnterMap().Coroutine(); });
                var enter2d = rc.Get<GameObject>("Enter2D");
                enter2d.GetComponent<Button>().onClick.AddListener(() => { self.Enter2D().Coroutine(); });
            }
        }
        
        public static async ETTask EnterMap(this UILobbyComponent self)
        {
            await EnterMapHelper.EnterMapAsync(self.ZoneScene());
            await UIHelper.Remove(self.ZoneScene(), UIType.UILobby);
        }
        public static async ETTask Enter2D(this UILobbyComponent self)
        {
            await EnterMapHelper.Enter2DAsync(self.ZoneScene());
            await UIHelper.Remove(self.ZoneScene(), UIType.UILobby);
        }
    }
}