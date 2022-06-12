﻿using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace ET
{
    public class Unit2DSystem: AwakeSystem<Unit2D, int>
    {
        public override void Awake(Unit2D self, int configId)
        {
            self.ConfigId = configId;
        }
    }
    public class Unit2D: Entity, IAwake<int>,ITransform
    {
        public int ConfigId { get; set; } //配置表id

        [BsonIgnore]
        public UnitConfig Config => UnitConfigCategory.Instance.Get(this.ConfigId);

        public UnitType Type => (UnitType)UnitConfigCategory.Instance.Get(this.ConfigId).Type;

        [BsonElement]
        private Vector3 position; //坐标

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

       
        private Vector3 lastPosition;
        private float angle;

     
    }
}