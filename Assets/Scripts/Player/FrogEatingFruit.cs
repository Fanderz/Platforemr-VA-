using UnityEngine;
using System;

public class FrogEatingFruit : MonoBehaviour
{
    public event Action OnIncreaseScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fruit fruit))
            EatFruit();
    }

    private void EatFruit() =>
        OnIncreaseScore?.Invoke();
}
