using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class ChangePosition2D: AEvent<Unit2D, ET.EventType.ChangePosition2D>
    {
        protected override async ETTask Run(Unit2D unit, ET.EventType.ChangePosition2D args)
        {
            Vector3 oldPos = args.OldPos;
            GameObjectComponent gameObjectComponent = unit.GetComponent<GameObjectComponent>();
            if (gameObjectComponent == null)
            {
                return;
            }
            Transform transform = gameObjectComponent.GameObject.transform;
            transform.position = unit.Position;
            await ETTask.CompletedTask;
        }
    }
}