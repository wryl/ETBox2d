﻿using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate2D_CreateUnit2DView: AEvent<Unit2D, EventType.AfterUnitCreate2D>
    {
        protected override async ETTask Run(Unit2D unit, EventType.AfterUnitCreate2D args)
        {
            // Unit View层
            // 这里可以改成异步加载，demo就不搞了
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("Unit.unity3d", "Unit000");
            GameObject go = UnityEngine.Object.Instantiate(bundleGameObject, GlobalComponent.Instance.Unit, true);
            go.transform.position = unit.Position;
            unit.AddComponent<GameObjectComponent>().GameObject = go;
            unit.AddComponent<PositionFollowComponent,Vector3>(unit.Position);
            //unit.AddComponent<AnimatorComponent>();
            await ETTask.CompletedTask;
        }
    }
}