namespace ET
{
    /// <summary>
    /// 攻击
    /// </summary>
    [ComponentOf(typeof (Unit2D))]
    [ChildType(typeof(CharacterAttackPartComponent))]
    public class CharacterAttackComponent: Entity, IAwake
    {
        /// <summary>
        /// 当前攻击区段
        /// </summary>
        public int CurrentIndexAtack { get; set; }
        public bool IsRunning;
        public ETCancellationToken Token;

    }
    /// <summary>
    /// 攻击区段
    /// </summary>
    public class CharacterAttackPartComponent: Entity, IAwake<int,int>
    {
        public int Duratuion { get; set; } = 800;
        public int Dmg { get; set; } = 10;
        public long StartTime { get; set; }
    }
}