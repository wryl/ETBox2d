namespace ET
{
    [ComponentOf(typeof(Unit2D))]
    public class EntitySyncComponent : Entity, IUpdate, IAwake,ITransfer
    {
        public int Fps { get; set; } = 20;
        public long Interval { get; set; } = 100;
        public long Timer { get; set; } = 0;
    }
}