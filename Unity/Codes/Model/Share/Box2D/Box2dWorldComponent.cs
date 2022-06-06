using System.Collections.Generic;
using Box2DSharp.Dynamics;

namespace ET
{
    public class Box2dWorldComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public World World;
        public Box2dWorldContactListener Listener;
        public int FrameCount = 2000;
        public Profile MaxProfile = new Profile();
        public Profile TotalProfile = new Profile();
        public FixedUpdate FixedUpdate;
        public FpsCounter FpsCounter = new FpsCounter();
        public Dictionary<Body, Body2dComponent> bodyComponents { get; set; } = new();
     
        public float _dt = 1 / 60f;

      
    }
}