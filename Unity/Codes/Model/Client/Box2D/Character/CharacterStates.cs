namespace ET
{
    namespace EventType
    {
        public struct CharacterChangeFace
        {
            public bool FaceRight;
        }

    }
    public enum CharacterConditions
    {
        Normal,
        ControlledMovement,
        Frozen,
        Paused,
        Dead,
        Stunned
    }
    public class CharacterStates 
    {



        public enum MovementStates 
        {
            Null,
            Idle,
            Walking,
            Falling,
            Running,
            Crouching,
            Crawling, 
            Dashing,
            LookingUp,
            WallClinging,
            Jetpacking,
            Diving,
            Gripping,
            Dangling,
            Jumping,
            Pushing,
            DoubleJumping,
            WallJumping,
            LadderClimbing,
            SwimmingIdle,
            Gliding,
            Flying,
            FollowingPath,
            LedgeHanging,
            LedgeClimbing,
            Rolling
        }
    }
}