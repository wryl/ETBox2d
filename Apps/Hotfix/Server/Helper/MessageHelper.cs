

using System.Collections.Generic;

namespace ET.Server
{
    public static class MessageHelper
    {
        public static void Broadcast(Unit unit, IActorMessage message)
        {
            Dictionary<long, AOIEntity> dict = unit.GetBeSeePlayers();
            foreach (AOIEntity u in dict.Values)
            {
                SendToClient(u.Unit, message);
            }
        }
        
        public static void SendToClient(Entity unit, IActorMessage message)
        {
            SendActor(unit.GetComponent<UnitGateComponent>().GateSessionActorId, message);
        }
        
        
        /// <summary>
        /// 发送协议给ActorLocation
        /// </summary>
        /// <param name="id">注册Actor的Id</param>
        /// <param name="message"></param>
        public static void SendToLocationActor(long id, IActorLocationMessage message)
        {
            ActorLocationSenderComponent.Instance.Send(id, message);
        }
        
        /// <summary>
        /// 发送协议给Actor
        /// </summary>
        /// <param name="actorId">注册Actor的InstanceId</param>
        /// <param name="message"></param>
        public static void SendActor(long actorId, IActorMessage message)
        {
            ActorMessageSenderComponent.Instance.Send(actorId, message);
        }
        
        /// <summary>
        /// 发送RPC协议给Actor
        /// </summary>
        /// <param name="actorId">注册Actor的InstanceId</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async ETTask<IActorResponse> CallActor(long actorId, IActorRequest message)
        {
            return await ActorMessageSenderComponent.Instance.Call(actorId, message);
        }
        
        /// <summary>
        /// 发送RPC协议给ActorLocation
        /// </summary>
        /// <param name="id">注册Actor的Id</param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async ETTask<IActorResponse> CallLocationActor(long id, IActorLocationRequest message)
        {
            return await ActorLocationSenderComponent.Instance.Call(id, message);
        }
        public static void BroadcastToAll(Entity domain, IActorMessage message)
        {
            foreach (var unit in domain.GetComponent<Unit2DComponent>().Children)
            {
                var gatesession = unit.Value.GetComponent<UnitGateComponent>()?.GateSessionActorId;
                if (gatesession.HasValue)
                {
                    SendActor(gatesession.Value, message);
                }
            }
        }
        public static void BroadcastToAllNotSelf(Entity domain,long id, IActorMessage message)
        {
            foreach (var unit in domain.GetComponent<Unit2DComponent>().Children)
            {
                if (unit.Value.Id==id)
                {
                    continue;
                }
                var gatesession = unit.Value.GetComponent<UnitGateComponent>()?.GateSessionActorId;
                if (gatesession.HasValue)
                {
                    SendActor(gatesession.Value, message);
                }
            }
        }
    }
}