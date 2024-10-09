using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Fighter _fighter;

    private void OnEnable()
    {
        _fighter.TakingDamage += _health.DecreaseValue;
    }

    private void OnDisable()
    {
        _fighter.TakingDamage -= _health.DecreaseValue;
    }
}
