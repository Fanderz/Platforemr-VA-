using System;
using UnityEngine;

public class Fruit : BaseEatObjects
{
    public override event Action<BaseEatObjects> Eated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out FrogEatingFruit frog))
            Eated?.Invoke(this);
    }
}