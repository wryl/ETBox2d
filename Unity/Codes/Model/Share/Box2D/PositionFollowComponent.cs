using UnityEngine;

namespace ET
{
    public static class PositionFollowComponentSystem
    {
        public class PositionFollowComponentAwakeSystem: AwakeSystem<PositionFollowComponent>
        {
            public override void Awake(PositionFollowComponent self)
            {
            }
        }
        public class PositionFollowComponentUpdateSystem: UpdateSystem<PositionFollowComponent>
        {
            public override void Update(PositionFollowComponent self)
            {
                self.GetParent<Unit2D>().Position=
            }
        }

        public static void CalcLerp(this PositionFollowComponent self,Vector3 old,Vector3 newposiont)
        {
            
            Vector3.Lerp(old,newposiont,0.2f)
        }
    }

    [ComponentOf(typeof (Unit2D))]
    public class PositionFollowComponent: Entity, IAwake, IUpdate, IDestroy
    {
        public Vector3 StartPoint { get; set; }
        public Vector3 TargetPoint { get; set; }
    }
}