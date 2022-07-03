
using System.Numerics;

namespace ET
{
    public static class PositionFollowComponentSystem
    {
        public class PositionFollowComponentAwakeSystem : AwakeSystem<PositionFollowComponent, Vector3>
        {
            public override void Awake(PositionFollowComponent self, Vector3 targetposition)
            {
                self.FrameTime = 50f;
                //self.TargetPoint = targetposition;
            }
        }
        public class PositionFollowComponentUpdateSystem : UpdateSystem<PositionFollowComponent>
        {
            public override void Update(PositionFollowComponent self)
            {
                long newtime = TimeHelper.ClientFrameTime() - self.StartTime;
                var body = self.GetParent<Unit2D>().GetComponent<Body2dComponent>().Body;
                if (newtime < self.FrameTime)
                {
                    body.SetTransform(Vector2.Lerp(self.StartPoint, self.TargetPoint, newtime / self.FrameTime),0);
                    //self.GetParent<Unit2D>().Position = Vector3.Lerp(self.StartPoint, self.TargetPoint, newtime / self.FrameTime) ;
                }
            }
        }


        public static void CalcLerp(this PositionFollowComponent self, Vector2 newposiont)
        {
            self.StartPoint = self.GetParent<Unit2D>().GetComponent<Body2dComponent>().Body.GetPosition();
            self.TargetPoint = newposiont;
            self.StartTime = TimeHelper.ClientFrameTime();
        }
    }
}