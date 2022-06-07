namespace ET
{
    public class EntitySyncComponent : Entity, IUpdate, IAwake
    {
        public int Fps { get; set; } = 20;
        public long Interval { get; set; } = 100;
        public long Timer { get; set; } = 0;
    }
}