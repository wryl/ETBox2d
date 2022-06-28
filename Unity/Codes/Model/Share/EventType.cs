
using System.Numerics;

namespace ET
{
    namespace EventType
    {
        public struct AppStart
        {
        }

        public struct SceneChangeStart
        {
        }
        
        public struct SceneChangeFinish
        {
        }

        public struct PingChange
        {
            public long Ping;
        }
        
        public struct AfterCreateClientScene
        {
        }
        
        public struct AfterCreateCurrentScene
        {
        }
        
        public struct AfterCreateLoginScene
        {
        }

        public struct AppStartInitFinish
        {
        }

        public struct LoginFinish
        {
        }

        public struct LoadingBegin
        {
        }

        public struct LoadingFinish
        {
        }

        public struct EnterMapFinish
        {
        }

        public struct AfterUnitCreate
        {
        }
        public struct ChangePosition2D
        {
            public Vector3 OldPos;
        }
        
        public struct Enter2DFinish
        {
        }
        
        public struct AfterUnitCreate2D
        {
        }
        public struct AfterUnitCreate2DMyself
        {
        }
        public struct After2DWorldCreate
        {
        }
        public struct UnitDashStart
        {
        }
        public struct UnitDashEnd
        {
        }
        public struct UnitRunStart
        {
        }
        public struct UnitRunEnd
        {
        }
        public struct StateChangeEvent
        {
            
        }
        public struct UnitChangeAnmi
        {
            public CharacterMovementStates MovementStates;
        }
    }
}