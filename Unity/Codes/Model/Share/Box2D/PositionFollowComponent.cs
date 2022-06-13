using UnityEngine;

namespace ET
{
    public static class PositionFollowComponentSystem
    {
        public class PositionFollowComponentAwakeSystem : AwakeSystem<PositionFollowComponent, Vector3>
        {
            public override void Awake(PositionFollowComponent self, Vector3 targetposition)
            {
                self.FrameTime = 50f;
                self.TargetPoint = targetposition;
            }
        }
        public class PositionFollowComponentUpdateSystem : UpdateSystem<PositionFollowComponent>
        {
            public override void Update(PositionFollowComponent self)
            {
                long newtime = TimeHelper.ClientFrameTime() - self.StartTime;
                if (newtime < self.FrameTime)
                {
                    self.GetParent<Unit2D>().Position = Vector3.Lerp(self.StartPoint, self.TargetPoint, newtime / self.FrameTime) ;
                }
                else if(Vector3.Distance(self.GetParent<Unit2D>().Position,self.TargetPoint)>0.01f)
                {
                    self.GetParent<Unit2D>().Position = self.TargetPoint;
                }
            }
        }

        public static void CalcLerp(this PositionFollowComponent self, Vector3 old, Vector3 newposiont)
        {
            self.StartPoint = old;
            self.TargetPoint = newposiont;
            self.StartTime = TimeHelper.ClientFrameTime();
        }
    }

    [ComponentOf(typeof(Unit2D))]
    public class PositionFollowComponent : Entity, IAwake<Vector3>, IUpdate, IDestroy
    {
        public Vector3 StartPoint { get; set; }
        public Vector3 TargetPoint { get; set; }
        public long StartTime { get; set; }
        /// <summary>
        /// 追赶时间
        /// </summary>
        public float FrameTime { get; set; }
    }
}