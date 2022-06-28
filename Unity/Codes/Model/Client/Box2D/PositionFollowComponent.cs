
using System.Numerics;

namespace ET
{
    /// <summary>
    /// 位置追赶系统.用FrameTime的时间追赶到目标位置
    /// </summary>
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