using System;


namespace ET.Client
{
    public static class EnterMapHelper
    {
        public static async ETTask EnterMapAsync(Scene clientScene)
        {
            try
            {
                G2C_EnterMap g2CEnterMap = await clientScene.GetComponent<SessionComponent>().Session.Call(new C2G_EnterMap()) as G2C_EnterMap;
                clientScene.GetComponent<PlayerComponent>().MyId = g2CEnterMap.MyId;
                
                // 等待场景切换完成
                await clientScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
                
                Game.EventSystem.Publish(clientScene, new EventType.EnterMapFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
        public static async ETTask Enter2DAsync(Scene zoneScene)
        {
            try
            {
                G2C_Enter2D g2CEnterMap = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_Enter2D()) as G2C_Enter2D;
                zoneScene.GetComponent<PlayerComponent>().MyId = g2CEnterMap.MyId;
                await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
                Game.EventSystem.Publish(zoneScene, new EventType.Enter2DFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}