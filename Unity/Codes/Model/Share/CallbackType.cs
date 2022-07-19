﻿namespace ET
{
    public static class CallbackType
    {
        public const int SessionStreamDispatcherClientOuter = 1;
        public const int SessionStreamDispatcherServerOuter = 2;
        public const int SessionStreamDispatcherServerInner = 3;
        public const int SessionStreamDispatcherClientP2POuter = 4;

        public const int GetAllConfigBytes = 11;
        public const int GetOneConfigBytes = 12;

        public const int RecastFileLoader = 13;
        
        // 框架层100-200，逻辑层的timer type从200起
        public const int WaitTimer = 100;
        public const int SessionIdleChecker = 101;
        public const int ActorLocationSenderChecker = 102;
        public const int ActorMessageSenderChecker = 103;
        
        // 框架层100-200，逻辑层的timer type 200-300
        public const int MoveTimer = 201;
        public const int AITimer = 202;
        public const int SessionAcceptTimeout = 203;
        
        public const int Box2DBeginContact = 210;
        public const int Box2DEndContact = 211;
    }
}