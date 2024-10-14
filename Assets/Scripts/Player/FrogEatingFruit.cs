using UnityEngine;
using System;

public class FrogEatingFruit : MonoBehaviour
{
    public event Action ScoreIncreased;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fruit fruit))
        {
            EatFruit();
            fruit.EatedObj();
        }
    }

    private void EatFruit() =>
        ScoreIncreased?.Invoke();
}
