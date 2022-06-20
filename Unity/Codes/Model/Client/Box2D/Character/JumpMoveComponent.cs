namespace ET{

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