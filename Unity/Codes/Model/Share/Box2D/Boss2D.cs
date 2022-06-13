using UnityEngine;

namespace ET
{
    [ComponentOf(typeof (Unit2D))]
    public class Boss2D : Entity, IAwake,IUpdate
    {
        public long OwnerId { get; set; }
    }
    [ComponentOf(typeof (Unit2D))]
    public class Player2D : Entity, IAwake,IUpdate
    {
        public long OwnerId { get; set; }
    }
    [ComponentOf(typeof (Unit2D))]
    public class Controller2D : Entity, IAwake,IUpdate
    {
        public long OwnerId { get; set; }
    }
}