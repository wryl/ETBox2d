using UnityEngine;

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
            unitInfo.X = position.x;
            unitInfo.Y = position.y;
            unitInfo.Z = position.z;
            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.Ks.Add(key);
                unitInfo.Vs.Add(value);
            }

            return unitInfo;
        }
    }
}