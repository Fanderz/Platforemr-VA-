using System;
using System.Collections;
using UnityEngine;

public class VampireAbility : MonoBehaviour
{
    //[SerializeField] private float _vampireSize;
    //[SerializeField] private float _duration;
    [SerializeField] private float _healthValue;
    [SerializeField] private float _manaPrice;
    [SerializeField] private float _vampireDelay;
    [SerializeField] private int _vampireLifeTime;

    private bool _isVampire = false;
    private int _lifeTime;

    private Health _health;
    private Mana _mana;
    private InputReader _inputReader;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    public event Action<float> TakingHealth;

    private void Awake()
    {
        _wait = new WaitForSeconds(_vampireDelay);
        _mana = GetComponentInParent<Mana>();
        _inputReader = GetComponentInParent<InputReader>();
        _health = GetComponentInParent<Health>();

        _lifeTime = _vampireLifeTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInChildren<Fighter>() != null)
        {
            var enemy = collision.GetComponentInChildren<Fighter>();
            if (_inputReader.GetIsVampire() && _mana.Value == _mana.MaxValue)
            {
                _isVampire = true;
                _coroutine = StartCoroutine(SuckHealth(enemy));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fighter enemy))
        {
            _isVampire = false;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = null;
            _coroutine = StartCoroutine(ReloadMana());
        }
    }

    //private void TakeHealth(float value) =>
    //    TakingHealth?.Invoke(value);

    private IEnumerator SuckHealth(Fighter enemy)
    {
        while (_isVampire && _lifeTime > 0)
        {
            enemy.TakeDamage(_healthValue);
            _health.IncreaseHealth(_healthValue);
            _mana.DecreaseMana(_manaPrice);
            _lifeTime--;

            yield return _wait;
        }

        _lifeTime = _vampireLifeTime;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = null;
        _coroutine = StartCoroutine(ReloadMana());
    }

    private IEnumerator ReloadMana()
    {
        while (_mana.Value != _mana.MaxValue)
        {
            _mana.IncreaseMana(_manaPrice);

            yield return _wait;
        }

        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = null;
    }
}
