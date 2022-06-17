using UnityEngine;

namespace ET
{
    [ComponentOf(typeof (Unit2D))]
    public class Boss2D : Entity, IAwake,IUpdate
    {
        public long OwnerId { get; set; }
    }
    [ComponentOf(typeof (Unit2D))]
    public class Player2D : Entity, IAwake,IUpdate
    {
        public long OwnerId { get; set; }
    }
    /// <summary>
    /// 冲刺模块.用来让单位冲刺
    /// 主要作用是给父节点计算出要额外附加的速度
    /// </summary>
    [ComponentOf(typeof(Unit2D))]
    public class DashMoveComponent: Entity,IAwake
    {
        public bool IsDashing;
        public float speed;
        public ETCancellationToken Token;
    }
    /// <summary>
    /// 跳跃模块
    /// 主要作用是给父节点计算出要额外附加的速度
    /// </summary>
    [ComponentOf(typeof(Unit2D))]
    public class JumpMoveComponent: Entity,IAwake
    {
        public float from;
        public float to;
        public float current;
        public float speed;
        public int direction;
    }
}