using UnityEngine;

namespace ET.Client
{
    public static class Unit2DFactory
    {
        public static Unit2D Create2D(Scene currentScene, UnitInfo unitInfo,bool IsBeForce)
        {
	        Unit2DComponent unitComponent = currentScene.GetComponent<Unit2DComponent>();
	        Unit2D unit = unitComponent.AddChildWithId<Unit2D, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        
	        unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
	        
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
		        numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
	        }
	        unit.AddComponent<ObjectWait>();
	        var body2d=unit.AddComponent<Body2dComponent>();
	        body2d.IsBeForce = IsBeForce;
	        body2d.CreateBody(0.2f,0.5f);
	        if (!IsBeForce)
	        {
		        unit.AddComponent<PositionFollowComponent, Vector3>(unit.Position);
		        Box2DHelper.ChangeGravityScale(body2d.Body,0);
	        }
			Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate2D());
	        return unit;
        }
        public static Unit2D CreateMyUnit2D(Scene currentScene, UnitInfo unitInfo)
        {
	        Unit2DComponent unitComponent = currentScene.GetComponent<Unit2DComponent>();
	        Unit2D unit = unitComponent.AddChildWithId<Unit2D, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
		        numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
	        }
	        unit.AddComponent<ObjectWait>();
	        unit.AddComponent<EntitySyncComponent>();
	        var body2d=unit.AddComponent<Body2dComponent>();
	        body2d.IsBeForce = true;
	        body2d.CreateBody(0.2f,0.5f);
	        Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate2DMyself());
	        Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate2D());
	        return unit;
        }
    }
}
