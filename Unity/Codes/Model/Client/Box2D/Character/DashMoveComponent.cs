
namespace ET
{
    /// <summary>
    /// 冲刺模块.用来让单位冲刺
    /// 主要作用是给父节点计算出要额外附加的速度
    /// </summary>
    [ComponentOf(typeof (Unit2D))]
    public class CharacterDashComponent: Entity, IAwake
    {
        public bool IsRunning;
        public float speed;
        public ETCancellationToken Token;
    }

    /// <summary>
    /// 水平移动模块.玩家操作
    /// 主要作用是给父节点计算出要额外附加的速度
    /// </summary>
    [ComponentOf(typeof (Unit2D))]
    public class CharacterhorizontalMoveComponent: Entity, IAwake
    {
        public bool Left=false;
        public bool Right=false;
        public bool IsRunning;
        public bool CurrFaceLeft = true;
    }
    /// <summary>
    /// 重力
    /// </summary>
    [ComponentOf(typeof (Unit2D))]
    public class CharacterGravityComponent: Entity, IAwake
    {
        public float speed { get; set; } = -5f;
    }


}