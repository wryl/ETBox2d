using System;
using System.Collections.Generic;

namespace ET
{
    [ComponentOf]
    public class CmdCollectorComponent : Entity, IAwake,IUpdate
    {
        public CmdType Cmd { get; set; }
        public Queue<int> CmdQueue { get; set; } = new Queue<int>();


    }

    public static class CmdCollectorSystem
    {
        public class CmdCollectorComponentAwakeSystem : AwakeSystem<CmdCollectorComponent>
        {
            public override void Awake(CmdCollectorComponent self)
            {
               
            }
        }
        public static void AddCmd(this CmdCollectorComponent self, int messageCharacterCmd)
        {
            self.CmdQueue.Enqueue(messageCharacterCmd);
        }
        public static CmdType GetCmd(this CmdCollectorComponent self)
        {
            if (self.CmdQueue.Count>0)
            {
                return (CmdType)self.CmdQueue.Dequeue();
            }
            return CmdType.Idle;
        }
    }

    [Flags]
    public enum CmdType
    {
        Idle = 0,
        A= 1<<1,
        AUp = 1<<2,
        D= 1<<3,
        DUp = 1<<4,
        LeftShift = 1<<5,
        Space = 1<<6,
        SpaceUp = 1<<7,
        J = 1<<8
    }
}