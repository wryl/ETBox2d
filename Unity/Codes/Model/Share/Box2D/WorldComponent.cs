using System.Collections.Generic;
using Box2DSharp.Dynamics;

namespace ET
{
    /// <summary>
    /// 物理世界组件，代表一个物理世界
    /// </summary>
    public class WorldComponent : Entity,IAwake,IDestroy
    {
        public World m_World;

        public List<Body> BodyToDestroy = new List<Body>();
        public int VelocityIteration = 10;
        public int PositionIteration = 10;

    }
}