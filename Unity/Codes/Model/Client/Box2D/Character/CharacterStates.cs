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
    public enum CharacterMovementStates 
    {
        Null,
        Idle,
        Walking,
        Falling,
        Running,
        Dashing,
        Diving,
        Jumping,
    }
    
}