using Box2DSharp.Common;
using Box2DSharp.Testbed.Unity;
using Box2DSharp.Testbed.Unity.Inspection;

namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterCreate2DWorld_AddDebugDraw: AEvent<Box2dWorldComponent, EventType.After2DWorldCreate>
    {
        protected override async ETTask Run(Box2dWorldComponent worldComponent, EventType.After2DWorldCreate args)
        {
            worldComponent.World.SetDebugDrawer(new DebugDrawer() { Drawer = UnityDrawer.GetDrawer(),Flags = DrawFlag.DrawShape });
            await ETTask.CompletedTask;
        }
    }
}