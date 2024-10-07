using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Transform _legs;
    [SerializeField] private LayerMask _groundMask;

    public bool IsGround { get; private set; }

    private void FixedUpdate()
    {
        IsGround = Physics2D.OverlapBox(_legs.position,_legs.localScale / 2, Quaternion.identity.y, _groundMask);
    }
}
