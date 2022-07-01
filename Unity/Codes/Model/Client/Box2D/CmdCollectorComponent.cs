using System;

namespace ET
{
    [ComponentOf]
    public class CmdCollectorComponent : Entity, IAwake,IUpdate
    {
        public CmdType Cmd { get; set; } 
    }

    [Flags]
    public enum CmdType
    {
        Idle = 0,
        A= 1<<1,
        D= 1<<2,
        LeftShift = 1<<3,
        Space = 1<<4,
        SpaceUp = 1<<5,
        J = 1<<6
    }
}