
using System.Numerics;
namespace ET.Client
{
	[ComponentOf(typeof(Unit2D))]
	public class Controller2DComponent: Entity, IAwake, IUpdate
    {
        /// <summary>
        /// 左右移动
        /// </summary>
        public float X;
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
