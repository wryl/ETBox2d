using UnityEngine;

namespace ET
{
    public interface ITransform
    {
        Vector3 LastPosition { get; set; }
        Vector3 Position { get; set; }
        float Angle { get; set; }
        TransformComponent TransformComponent { get; }
    }
}