using UnityEngine;

namespace ET
{

    public class Boss2D : Entity, ITransform,IAwake,IUpdate
    {
        private Vector3 lastPosition;
        private Vector3 position;
        private float angle;
        public long OwnerId { get; set; }

        public Vector3 LastPosition
        {
            get => this.lastPosition;
            set => this.lastPosition = value;
        }

        public Vector3 Position
        {
            get => this.position;
            set
            {
                this.position = value;
            }
        }

        public float Angle
        {
            get => this.angle;
            set => this.angle = value;
        }
    }
}