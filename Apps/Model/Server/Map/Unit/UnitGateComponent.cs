namespace ET.Server
{
    [ComponentOf]
    public class UnitGateComponent : Entity, IAwake<long>, ITransfer
    {
        public long GateSessionActorId { get; set; }
    }
}