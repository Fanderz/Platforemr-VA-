using UnityEngine;
using System.Collections;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Mana _mana;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _vampireValueSize;
    [SerializeField] private float _manaPrice;
    [SerializeField] private float _vampireDelay;

    private bool _isVampire = false;
    private Fighter _enemy;
    private Health _enemyHealth;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_vampireDelay);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_inputReader.GetIsVampire() && _mana.Value == _mana.MaxValue)
        {
            if (collision.GetComponentInChildren<Fighter>() != null)
            {
                _enemy = collision.GetComponentInChildren<Fighter>();
                _enemyHealth = collision.GetComponent<Health>();

                _isVampire = true;
                _coroutine = StartCoroutine(SuckHealth(_enemy));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isVampire)
        {
            if (_enemy != null && collision.GetComponent<Fighter>() == _enemy)
            {
                _isVampire = false;

                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = StartCoroutine(ReloadMana());
            }
        }
    }

    private IEnumerator SuckHealth(Fighter enemy)
    {
        while (_isVampire && _mana.Value > 0)
        {
            if (_enemyHealth.Value >= _vampireValueSize)
            {
                enemy.TakeDamage(_vampireValueSize);
                _health.IncreaseValue(_vampireValueSize);
            }
            else
            {
                enemy.TakeDamage(_enemyHealth.Value);
                _health.IncreaseValue(_enemyHealth.Value);
            }

            _mana.DecreaseValue(_manaPrice);

            yield return _wait;
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ReloadMana());
    }

    private IEnumerator ReloadMana()
    {
        while (_mana.Value != _mana.MaxValue)
        {
            _mana.IncreaseValue(_manaPrice);

            yield return _wait;
        }

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}
