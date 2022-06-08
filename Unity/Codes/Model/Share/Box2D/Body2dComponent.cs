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
				var p3 = (GetParent<Entity>() as ITransform).Position;
				return new Vector2(p3.x, p3.z);
			}
			set
			{
				(GetParent<Entity>() as ITransform).Position = new Vector3(value.x, 0, value.y);
			}
		}

		public float Angle
		{
			get
			{
				return (GetParent<Entity>() as ITransform).Angle;
			}
			set
			{
				(GetParent<Entity>() as ITransform).Angle = value;
			}
		}
		public Vector2 lastPosition { get; set; }

	}
}