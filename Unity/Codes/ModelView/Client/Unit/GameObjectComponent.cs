using UnityEngine;

namespace ET.Client
{
    [ComponentOf()]
    public class GameObjectComponent: Entity, IAwake, IDestroy
    {
        public GameObject GameObject { get; set; }
    }
}