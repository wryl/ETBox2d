using System;


namespace ET.Client
{
    public static class EnterMapHelper
    {
        public static async ETTask EnterMapAsync(Scene zoneScene)
        {
            try
            {
                G2C_EnterMap g2CEnterMap = await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2G_EnterMap()) as G2C_EnterMap;
                zoneScene.GetComponent<PlayerComponent>().MyId = g2CEnterMap.MyId;
                
                // 等待场景切换完成
                await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
                
                Game.EventSystem.Publish(zoneScene, new EventType.EnterMapFinish());
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
                await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
                Game.EventSystem.Publish(zoneScene, new EventType.EnterMapFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}