using Box2DSharp.Common;
using Box2DSharp.Testbed.Unity;
using Box2DSharp.Testbed.Unity.Inspection;
using UnityEngine;

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
    [Event(SceneType.Current)]
    public class UnitDashStartEvent: AEvent<Unit2D, EventType.UnitDashStart>
    {
        protected override async ETTask Run(Unit2D unit, EventType.UnitDashStart args)
        {
            //播放一些特效之类的
            Log.Debug("EventType.UnitDashStart");
           //unit.GetComponent<GameObjectComponent>().GameObject
           await ETTask.CompletedTask;
        }
    }
    [Event(SceneType.Current)]
    public class UnitDashEndEvent: AEvent<Unit2D, EventType.UnitDashEnd>
    {
        protected override async ETTask Run(Unit2D unit, EventType.UnitDashEnd args)
        {
            //播放一些特效之类的
            Log.Debug("EventType.UnitDashEnd");
            //unit.GetComponent<GameObjectComponent>().GameObject
            await ETTask.CompletedTask;
        }
    }
    [Event(SceneType.Current)]
    public class CharacterChangeFaceEvent: AEvent<Unit2D, EventType.CharacterChangeFace>
    {
        protected override async ETTask Run(Unit2D unit, EventType.CharacterChangeFace args)
        {
            await ETTask.CompletedTask;
            GameObject gameObject = unit.GetComponent<GameObjectComponent>().GameObject;
            if (args.FaceRight)
            {
                gameObject.transform.localScale = Vector3.one;
            }
            else
            {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}