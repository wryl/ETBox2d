using System;

namespace ET.Client
{
    public static class P2PHelper
    {
        public static void Login(Scene clientScene, string address)
        {
            try
            {
                var p2PKcpComponent = clientScene.GetComponent<NetP2PKcpComponent>();
                Session session = p2PKcpComponent.Create(NetworkHelper.ToIPEndPoint(address));
                session.Send(new P2PStartMessage());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}