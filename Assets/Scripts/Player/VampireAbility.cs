using UnityEngine;
using System.Collections;
using System.Linq;
using System;

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

    private SpriteRenderer _spriteView;

    private WaitForSeconds _wait;
    private Coroutine _coroutine;

    private void Awake()
    {
        _wait = new WaitForSeconds(_vampireDelay);
        _spriteView = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Fighter>() != null)
            _spriteView.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Fighter>() == null)
            _spriteView.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_inputReader.GetIsVampire() && _mana.Value == _mana.MaxValue)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _isVampire = true;
            _coroutine = StartCoroutine(SuckHealth());
        }

        if (_mana.Value == 0)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ReloadMana());
        }
    }

    private IEnumerator SuckHealth()
    {
        while (_isVampire && _mana.Value > 0)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, transform.GetComponent<CircleCollider2D>().radius, _enemiesLayer);

            if (enemies.Length != 0)
            {
                int nearestIndex = CalculateNearest(enemies);

                if (enemies[nearestIndex].TryGetComponent(out Fighter enemy))
                {
                    if (enemy.CurrentHealth.Value >= _vampireValueSize)
                    {
                        enemy.TakeDamage(_vampireValueSize);
                        _health.IncreaseValue(_vampireValueSize);
                    }
                    else
                    {
                        enemy.TakeDamage(enemy.CurrentHealth.Value);
                        _health.IncreaseValue(enemy.CurrentHealth.Value);
                    }

                    _mana.DecreaseValue(_manaPrice);
                }
            }
            else
            {
                _isVampire = false;

                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = StartCoroutine(ReloadMana());
            }

            yield return _wait;
        }
    }

    private int CalculateNearest(Collider2D[] enemies)
    {
        float[] distances = new float[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
            distances[i] = Vector2.Distance(this.transform.position, enemies[i].transform.position);

        return Array.IndexOf(distances, distances.Min());
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
