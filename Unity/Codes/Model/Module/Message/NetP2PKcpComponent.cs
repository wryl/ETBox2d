using System.Net;

namespace ET
{
    [ChildType(typeof(Session))]
    [ComponentOf(typeof(Scene))]
    public class NetP2PKcpComponent: Entity, IAwake<int>, IAwake<IPEndPoint, int>, IDestroy, IAwake<IAction<int>>
    {
        public AService Service;
        
        public int SessionStreamDispatcherType { get; set; }
    }
}