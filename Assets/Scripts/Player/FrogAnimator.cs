using UnityEngine;

public class FrogAnimator : MonoBehaviour
{
    private Animator _animator;
    private Frog _frog;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _frog = GetComponentInParent<Frog>();
    }

    private void OnEnable()
    {
        _frog.OnAnimatorParameterChange += ChangeParameter;
    }

    private void OnDisable()
    {
        _frog.OnAnimatorParameterChange -= ChangeParameter;
    }

    private void ChangeParameter(bool isJumping,float moveX)
    {
        _animator.SetBool(PlayerAnimatorData.Params.IsJumping, isJumping);
        _animator.SetFloat(PlayerAnimatorData.Params.MoveX, moveX);
    }
}
