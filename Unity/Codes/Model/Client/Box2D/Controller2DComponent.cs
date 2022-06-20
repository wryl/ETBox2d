
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

namespace ET.Client
{
	[ComponentOf(typeof(Unit2D))]
	public class Controller2DComponent: Entity, IAwake, IUpdate
    {
        public Unit2D MyUnit2D => this.GetParent<Unit2D>();
        public Vector2 Velocity { get; set; }
        public bool DirectionLeft { get; set; }
    }
}
