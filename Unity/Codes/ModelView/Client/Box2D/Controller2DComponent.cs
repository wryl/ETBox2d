
using System.Numerics;
namespace ET.Client
{
	[ComponentOf(typeof(Unit2D))]
	public class Controller2DComponent: Entity, IAwake, IUpdate,IFixedUpdate
    {
        public Unit2D MyUnit2D => this.GetParent<Unit2D>();
        public Vector2 Velocity { get; set; }
        public Vector2 laterDirection { get; set; }

        /// <summary>
        /// 突进
        /// </summary>
        public bool Dash;
        /// <summary>
        /// 跳跃
        /// </summary>
        public bool Jump;
    }

    
}
