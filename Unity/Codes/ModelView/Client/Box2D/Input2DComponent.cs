using System.Numerics;

namespace ET.Client
{
    [ComponentOf(typeof(Unit2D))]
    public class Input2DComponent: Entity, IAwake, IUpdate
    {
        public Unit2D MyUnit2D;
        public Vector2 dir;
    }
}