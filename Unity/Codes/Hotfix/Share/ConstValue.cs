namespace ET
{
    public static class ConstValue
    {
        public const string RouterHttpAddress = "127.0.0.1:10300";
        public const int SessionTimeoutTime = 30 * 1000;
        /// <summary>
        /// 固定间隔的目标FPS
        /// </summary>
        public const int FixedUpdateTargetFPS = 30;

        public const float FixedUpdateTargetDTTime_Float = 1f / FixedUpdateTargetFPS;

        public const long FixedUpdateTargetDTTime_Long = (long) (FixedUpdateTargetDTTime_Float * 1000);
    }
}