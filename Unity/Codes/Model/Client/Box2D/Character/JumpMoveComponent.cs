namespace ET{

    /// <summary>
    /// 跳跃模块
    /// 主要作用是给父节点计算出要额外附加的速度
    /// </summary>
    [ComponentOf(typeof(Unit2D))]
    public class CharacterJumpComponent: Entity,IAwake,IUpdate
    {
        public bool IsRunning { get; set; }
        public bool IsStoring { get; set; }
        public float BaseSpeed { get; set; }
        /// <summary>
        /// 最小跳跃时间.立刻松开也按照这个时间计算
        /// </summary>
        public int MinJumpHold { get; set; }
        public int MaxJumpHold { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public long JumpNum { get; set; }
        public ETCancellationToken Token { get; set; }
    }
}