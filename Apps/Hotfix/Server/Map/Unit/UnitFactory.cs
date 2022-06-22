using System;
using UnityEngine;

namespace ET.Server
{
    public static class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                {
                    Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                    unit.AddComponent<MoveComponent>();
                    unit.Position = new Vector3(-10, 0, -10);
			
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    numericComponent.Set(NumericType.AOI, 15000); // 视野15米
                    
                    unitComponent.Add(unit);
                    // 加入aoi
                    unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);
                    return unit;
                }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
        public static Unit2D Create2D(Scene scene, long id, UnitType unitType)
        {
            Unit2DComponent unitComponent = scene.GetComponent<Unit2DComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                {
                    Unit2D unit = unitComponent.AddChildWithId<Unit2D, int>(id, RandomHelper.RandomNumber(10,21));
                    unit.Position = new Vector3(RandomHelper.RandomNumber(-2,4), 5, 0);
                    NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                    numericComponent.Set(NumericType.Speed, 6f); // 速度是6米每秒
                    unit.AddComponent<ServerEntitySyncComponent>();
                    return unit;
                }
                case UnitType.Monster:
                    Unit2D monster = unitComponent.AddChildWithId<Unit2D, int>(id, RandomHelper.RandomNumber(0,10));
                    monster.Position = new Vector3(5, 10, 0);
                    var nc=monster.AddComponent<NumericComponent>();
                    nc.Set(NumericType.Speed, 8f); 
                    monster.AddComponent<Boss2D>();
                    var body2d=monster.AddComponent<Body2dComponent>();
                    body2d.IsBeForce = true;
                    body2d.CreateBody(0.2f,0.5f);
                    monster.AddComponent<CharacterGravityComponent>();
                    monster.AddComponent<CharacterJumpComponent>();
                    monster.AddComponent<ServerEntitySyncComponent>();
                    monster.AddComponent<Client.Controller2DComponent>();
                    return monster;
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
    }
}