using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ChangePosition2D: AEvent<Unit2D, ET.EventType.ChangePosition2D>
    {
        protected override async ETTask Run(Unit2D unit, ET.EventType.ChangePosition2D args)
        {
            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }
            Transform transform = gameObjectComponent.GameObject.transform;
            transform.position = new Vector3(unit.Position.X,unit.Position.Y,unit.Position.Z);
            await ETTask.CompletedTask;
        }
    }
}