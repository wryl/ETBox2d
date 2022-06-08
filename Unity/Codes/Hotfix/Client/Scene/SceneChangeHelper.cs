﻿namespace ET.Client
{
    public static class SceneChangeHelper
    {
        // 场景切换协程
        public static async ETTask SceneChangeTo(Scene zoneScene, string sceneName, long sceneInstanceId)
        {
            zoneScene.RemoveComponent<AIComponent>();
            
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的
            Scene currentScene = SceneFactory.CreateCurrentScene(sceneInstanceId, zoneScene.Zone, sceneName, currentScenesComponent);
            UnitComponent unitComponent = currentScene.AddComponent<UnitComponent>();
         
            // 可以订阅这个事件中创建Loading界面
            Game.EventSystem.Publish(zoneScene, new EventType.SceneChangeStart());

            // 等待CreateMyUnit的消息
            WaitType.Wait_CreateMyUnit waitCreateMyUnit = await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_CreateMyUnit>();
            M2C_CreateMyUnit m2CCreateMyUnit = waitCreateMyUnit.Message;
            Unit unit = Client.UnitFactory.Create(currentScene, m2CCreateMyUnit.Unit);
            unitComponent.Add(unit);
            
            zoneScene.RemoveComponent<AIComponent>();
            
            Game.EventSystem.Publish(currentScene, new EventType.SceneChangeFinish());

            // 通知等待场景切换的协程
            zoneScene.GetComponent<ObjectWait>().Notify(new WaitType.Wait_SceneChangeFinish());
        }
        public static async ETTask SceneChangeTo2D(Scene zoneScene, string sceneName, long sceneInstanceId)
        {
            
            CurrentScenesComponent currentScenesComponent = zoneScene.GetComponent<CurrentScenesComponent>();
            currentScenesComponent.Scene?.Dispose(); // 删除之前的CurrentScene，创建新的
            Scene currentScene = SceneFactory.CreateCurrentScene(sceneInstanceId, zoneScene.Zone, sceneName, currentScenesComponent);
            Unit2DComponent unitComponent = currentScene.AddComponent<Unit2DComponent>();
         
            // 可以订阅这个事件中创建Loading界面
            Game.EventSystem.Publish(zoneScene, new EventType.SceneChangeStart());

            // 等待CreateMyUnit的消息
            WaitType.Wait_Create2DWorld waitCreateMyUnit = await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_Create2DWorld>();
            M2C_CreateMyUnit2D m2CCreateMyUnit = waitCreateMyUnit.Message;
            Unit2D unit = Client.UnitFactory.Create2D(currentScene, m2CCreateMyUnit.Unit);
            unitComponent.Add(unit);
            
            
            Game.EventSystem.Publish(currentScene, new EventType.SceneChangeFinish());

            // 通知等待场景切换的协程
            zoneScene.GetComponent<ObjectWait>().Notify(new WaitType.Wait_SceneChangeFinish());
        }
    }
}