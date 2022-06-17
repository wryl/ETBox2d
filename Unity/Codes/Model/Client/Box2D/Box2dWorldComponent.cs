using System.Collections.Generic;
using Box2DSharp.Dynamics;

namespace ET
{
    [ComponentOf(typeof (Scene))]
    public class Box2dWorldComponent: Entity, IAwake, IDestroy, IFixedUpdate
    {
        public World World{ get; set; }
        public int FrameCount = 2000;
        public Profile MaxProfile = new Profile();
        public Profile TotalProfile = new Profile();
        public FpsCounter FpsCounter = new FpsCounter();
        public Dictionary<Body, Body2dComponent> bodyComponents { get; set; } = new();
     
        public float _dt = 1 / 60f;

      
    }
}