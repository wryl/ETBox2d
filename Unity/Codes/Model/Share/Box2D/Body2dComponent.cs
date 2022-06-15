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
		public Unit2D ParentUnit;
		public Body Body { get; set; }
		/// <summary>
		/// 是否受到力影响
		/// </summary>
		public bool IsBeForce{ get; set; }

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