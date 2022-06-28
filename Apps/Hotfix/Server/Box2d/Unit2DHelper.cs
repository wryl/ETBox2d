using System;
using System.Numerics;

namespace ET.Server
{
    [FriendOf(typeof (NumericComponent))]
    public static class Unit2DHelper
    {
        public static UnitInfo CreateUnitInfo(Unit2D unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;
            unitInfo.Type = (int) unit.Type;
            Vector3 position = unit.Position;
            unitInfo.X = position.X;
            unitInfo.Y = position.Y;
            unitInfo.Z = position.Z;
            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.Ks.Add(key);
                unitInfo.Vs.Add(value);
            }

            return unitInfo;
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
                    monster.AddComponent<StateMachine2D>();
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