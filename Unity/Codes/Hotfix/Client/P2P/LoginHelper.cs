using System;

namespace ET.Client
{
    public static class P2PHelper
    {
        public static void Login(Scene clientScene, string address)
        {
            try
            {
                Session session = null;

                var p2PKcpComponent = clientScene.AddComponent<NetP2PKcpComponent, int>(CallbackType.SessionStreamDispatcherClientOuter);
                // 获取路由跟realmDispatcher地址
                session = p2PKcpComponent.Create(NetworkHelper.ToIPEndPoint(address));
                session.Send(new P2PStartMessage());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}