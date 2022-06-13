﻿using UnityEngine;

namespace ET.Client
{
    public static class UnitFactory
    {
        public static Unit Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);
	        
	        unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
	        unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);
	        
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
		        numericComponent.Set(unitInfo.Ks[i], unitInfo.Vs[i]);
	        }
	        
	        unit.AddComponent<MoveComponent>();
	        if (unitInfo.MoveInfo != null)
	        {
		        if (unitInfo.MoveInfo.X.Count > 0)
		        {
			        using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
			        {
				        list.Add(unit.Position);
				        for (int i = 0; i < unitInfo.MoveInfo.X.Count; ++i)
				        {
					        list.Add(new Vector3(unitInfo.MoveInfo.X[i], unitInfo.MoveInfo.Y[i], unitInfo.MoveInfo.Z[i]));
				        }

				        unit.MoveToAsync(list).Coroutine();
			        }
		        }
	        }

	        unit.AddComponent<ObjectWait>();

	        unit.AddComponent<XunLuoPathComponent>();
	        
	        Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate());
            return unit;
        }
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
	        }
			Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate2D());
	        return unit;
        }
    }
}
