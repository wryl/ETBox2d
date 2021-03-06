using System.Net;

namespace ET.Server
{
    public static class SceneFactory
    {
        public static async ETTask<Scene> Create(Entity parent, string name, SceneType sceneType)
        {
            long instanceId = IdGenerater.Instance.GenerateInstanceId();
            return await Create(parent, instanceId, instanceId, parent.DomainZone(), name, sceneType);
        }
        
        public static async ETTask<Scene> Create(Entity parent, long id, long instanceId, int zone, string name, SceneType sceneType, StartSceneConfig startSceneConfig = null)
        {
            await ETTask.CompletedTask;
            Scene scene = EntitySceneFactory.CreateScene(id, instanceId, zone, sceneType, name, parent);

            scene.AddComponent<MailBoxComponent, MailboxType>(MailboxType.UnOrderMessageDispatcher);

            switch (scene.SceneType)
            {
                case SceneType.Router:
                    scene.AddComponent<RouterComponent, IPEndPoint, string>(new IPEndPoint(IPAddress.Any, startSceneConfig.OuterPort),
                        startSceneConfig.StartProcessConfig.InnerIP
                    );
                    break;
                case SceneType.RouterManager:
                    scene.AddComponent<HttpComponent, string>($"http://*:{startSceneConfig.OuterPort}/");
                    break;
                case SceneType.Realm:
                    scene.AddComponent<NetKcpComponent, IPEndPoint, int>(new IPEndPoint(IPAddress.Any, startSceneConfig.OuterPort), CallbackType.SessionStreamDispatcherServerOuter);
                    break;
                case SceneType.Gate:
                    scene.AddComponent<NetKcpComponent, IPEndPoint, int>(new IPEndPoint(IPAddress.Any, startSceneConfig.OuterPort), CallbackType.SessionStreamDispatcherServerOuter);
                    scene.AddComponent<PlayerComponent>();
                    scene.AddComponent<GateSessionKeyComponent>();
                    break;
                case SceneType.Map:
                    scene.AddComponent<UnitComponent>();
                    scene.AddComponent<AOIManagerComponent>();
                    break;
                case SceneType.Location:
                    scene.AddComponent<LocationComponent>();
                    break;
                case SceneType.Robot:
                    scene.AddComponent<RobotManagerComponent>();
                    
                    break;
                case SceneType.Box2dWorld:
                    scene.AddComponent<Unit2DComponent>();
                    var world=scene.AddComponent<Box2dWorldComponent>();
                    // Unit2DHelper.Create2D(scene, IdGenerater.Instance.GenerateId(), UnitType.Monster);
                    break;
            }

            return scene;
        }
    }
}