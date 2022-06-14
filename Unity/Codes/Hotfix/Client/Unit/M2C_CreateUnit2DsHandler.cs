namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_CreateUnit2DsHandler : AMHandler<M2C_CreateUnit2Ds>
    {
        protected override async ETTask Run(Session session, M2C_CreateUnit2Ds message)
        {
            Scene currentScene = session.DomainScene().CurrentScene();
            Unit2DComponent unitComponent = currentScene.GetComponent<Unit2DComponent>();
			
            foreach (UnitInfo unitInfo in message.Units)
            {
                if (unitComponent.Get(unitInfo.UnitId) != null)
                {
                    continue;
                }
                Unit2D unit = Unit2DFactory.Create2D(currentScene, unitInfo,false);
            }
            await ETTask.CompletedTask;
        }
    }
}