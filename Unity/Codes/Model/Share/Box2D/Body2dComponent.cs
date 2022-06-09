using Box2DSharp.Collision.Collider;
using Box2DSharp.Collision.Shapes;
using Box2DSharp.Common;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Joints;
using UnityEngine;

namespace ET
{
	[ComponentOf()]
	public class Body2dComponent : Entity, IAwake, IUpdate, IDestroy
	{
		public Box2dWorldComponent Box2DWorldComponent { get; set; }
		public System.Action<Body2dComponent> OnBeginContactAction { get; set; }
		public Body Body { get; set; }
		/// <summary>
		/// 是否受到力影响
		/// </summary>
		public bool IsBeForce{ get; set; }
		public Vector2 Position
		{
			get
			{
				var p3 = GetParent<Unit2D>().Position;
				return new Vector2(p3.x, p3.y);
			}
			set
			{
				GetParent<Unit2D>().Position = new Vector3(value.x,value.y, 0 );
			}
		}

		public float Angle
		{
			get
			{
				return GetParent<Unit2D>().Angle;
			}
			set
			{
				GetParent<Unit2D>().Angle = value;
			}
		}
		public Vector2 lastPosition { get; set; }

	}
}