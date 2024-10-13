using System;
using System.Collections;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Health _currentHealth;

    private WaitForSeconds _wait;
    private Coroutine _fightingCoroutine;

    public event Action<float> TakingDamage;
    
    public bool IsFighting { get; private set; }

    public Health CurrentHealth => _currentHealth;

    private void Awake()
    {
        _wait = new WaitForSeconds(_attackDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Fighter enemy))
        {
            IsFighting = true;
            _fightingCoroutine = StartCoroutine(Attack(enemy));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Fighter enemy))
        {
            IsFighting = false;

            if (_fightingCoroutine != null)
                StopCoroutine(_fightingCoroutine);
        }
    }

    public void TakeDamage(float damage) =>
        TakingDamage?.Invoke(damage);

    private IEnumerator Attack(Fighter enemy)
    {
        while (IsFighting)
        {
            enemy.TakeDamage(_damage);

            yield return _wait;
        }
    }
}
