using System.Numerics;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{

    public class Unit2D: Entity, IAwake<int>
    {
        public int ConfigId { get; set; } //prefab id

        public UnitType Type { get; set; }

        [BsonIgnore]
        private Vector3 position; //坐标
        [BsonIgnore]
        public Vector3 LastPosition
        {
            get => this.lastPosition;
            set => this.lastPosition = value;
        }

        [BsonIgnore]
        public Vector3 Position
        {
            get => this.position;
            set
            {
                Vector3 oldPos = this.position;
                this.position = value;
                Game.EventSystem.Publish(this, new EventType.ChangePosition2D() { OldPos = oldPos });
            }
        }

        public float Angle
        {
            get => this.angle;
            set => this.angle = value;
        }

        [BsonIgnore]
        private Vector3 lastPosition;
        private float angle;

     
    }
}