using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ET
{
    [ComponentOf(typeof(Unit2D))]
    public class ServerEntitySyncComponent : Entity, IUpdate, IAwake,ITransfer
    {
        public int Fps { get; set; } = 20;
        public long Interval { get; set; } = 100;
        public long Timer { get; set; } = 0;
    }
    [ComponentOf(typeof(Scene))]
    public class P2PManager: Entity,IAwake
    {
        /// <summary>
        /// 多少人启动p2p,先尝试2人互联
        /// </summary>
        public int MaxPlayer{ get; set; } = 2;
        public int CurrentNum{ get; set; }
    }
}