using System.Numerics;
using ET.Client;

namespace ET
{
    [FriendOf(typeof(ET.RayCastDownComponent))]
    public static class RayCastDownComponentSystem
    {
        public class RayCastDownComponentAwakeSystem : AwakeSystem<RayCastDownComponent>
        {
            public override void Awake(RayCastDownComponent self)
            {
                self.ParentUnit = self.GetParent<Unit2D>();
                self.ClosestGroundCallback = new RayCastClosestGroundCallback();
                self.World = self.DomainScene().GetComponent<Box2dWorldComponent>().World;
                self.Body = self.ParentUnit.GetComponent<Body2dComponent>().Body;
                self.DownPosition = new Vector2(0, 0.25f);
                self.TargetPosition = new Vector2(0, 0.28f);
            }
        }
        public class RayCastDwonComponentDestroySystem : DestroySystem<RayCastDownComponent>
        {
            public override void Destroy(RayCastDownComponent self)
            {
                self.ParentUnit = null;
                self.ClosestGroundCallback = null;
                self.World = null;
            }
        }
        public class RayCastDwonComponentUpdateSystem : UpdateSystem<RayCastDownComponent>
        {
            public override void Update(RayCastDownComponent self)
            {
                self.Update();
            }
        }
        public static void Update(this RayCastDownComponent self)
        {
            self.ClosestGroundCallback.Hit = false;
            self.World.RayCast(self.ClosestGroundCallback,self.Body.GetPosition()-new Vector2(0,0),self.Body.GetPosition()-new Vector2(0,0.52f));
            bool lastState = self.ParentUnit.GetComponent<Controller2DComponent>().IsGround;
            self.ParentUnit.GetComponent<Controller2DComponent>().IsGround = self.ClosestGroundCallback.Hit;
            Log.Debug("射线状态"+self.ClosestGroundCallback.Hit);

            if (lastState==false&&self.ClosestGroundCallback.Hit==true)
            {
                Log.Debug("重置跳跃");
                self.ParentUnit.GetComponent<CharacterJumpComponent>().RestoreJumpNum();
            }
        }
    }
}