using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int MoveX = Animator.StringToHash(nameof(MoveX));
        public static readonly int IsJumping = Animator.StringToHash(nameof(IsJumping));
    }
}
