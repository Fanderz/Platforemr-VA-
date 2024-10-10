using UnityEngine;
using System.Collections;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Mana _mana;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LayerMask _enemiesLayer;

    [SerializeField] private float _vampireValueSize;
    [SerializeField] private float _manaPrice;
    [SerializeField] private float _vampireDelay;

    private bool _isVampire = false;

    private Health _enemyHealth;
    private SpriteRenderer _spriteView;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_vampireDelay);
        _spriteView = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        var enemy = Physics2D.OverlapCircle(transform.position, transform.GetComponent<CircleCollider2D>().radius, _enemiesLayer);

        if (enemy != null)
        {
            _spriteView.enabled = true;

            if (_inputReader.GetIsVampire())
            {
                if (_mana.Value == _mana.MaxValue)
                {
                    if (_coroutine != null)
                        StopCoroutine(_coroutine);

                    _enemyHealth = enemy.GetComponentInParent<Health>();

                    _isVampire = true;
                    _coroutine = StartCoroutine(SuckHealth(enemy.GetComponent<Fighter>()));
                }
                else
                {
                    if (_coroutine != null)
                        StopCoroutine(_coroutine);

                    _coroutine = StartCoroutine(ReloadMana());
                }
            }
        }
        else
        {
            _isVampire = false;

            _spriteView.enabled = false;
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
