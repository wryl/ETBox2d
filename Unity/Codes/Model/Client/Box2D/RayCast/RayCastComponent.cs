using Box2DSharp.Dynamics;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace ET
{
	[ComponentOf(typeof (Unit2D))]
	public class RayCastDownComponent : Entity, IAwake, IUpdate, IDestroy
	{
		public Unit2D ParentUnit;
		public Body Body;
		public World World;
		public RayCastClosestGroundCallback ClosestGroundCallback;
		public Vector2 DownPosition;
		public Vector2 TargetPosition;
	}
}