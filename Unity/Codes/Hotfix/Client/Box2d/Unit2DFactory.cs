
using System.Numerics;

namespace ET.Client
{
    public static class Unit2DFactory
    {
        public static Unit2D CreateUnit2D(Scene currentScene, UnitInfo unitInfo, bool IsSelf)
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

	        unit.AddComponent<StateMachine2D>();
	        unit.AddComponent<Controller2DComponent>();
	        unit.AddComponent<CharacterDashComponent>();
	        unit.AddComponent<CharacterhorizontalMoveComponent>();
	        unit.AddComponent<CharacterGravityComponent>();
	        unit.AddComponent<CharacterJumpComponent>();
	        unit.AddComponent<CharacterAttackComponent>();
	        var body2d=unit.AddComponent<Body2dComponent>();
	        body2d.IsBeForce = true;
	        body2d.CreateBody(0.2f,0.5f);
	        unit.AddComponent<RayCastDownComponent>();
	        if (IsSelf)
	        {
		        unit.AddComponent<EntitySyncComponent>();
		        Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate2DMyself());

	        }
	        else
	        {
		        unit.AddComponent<CmdCollectorComponent>();
		        unit.AddComponent<PositionFollowComponent, Vector3>(unit.Position);
	        }
	        Game.EventSystem.Publish(unit, new EventType.AfterUnitCreate2D());
	        return unit;
        }
    }
}
