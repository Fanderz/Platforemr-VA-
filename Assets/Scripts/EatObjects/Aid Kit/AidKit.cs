using UnityEngine;
using System;

public class AidKit : BaseEatObjects
{
    [SerializeField] private float _minKitSize;
    [SerializeField] private float _maxKitSize;

    public override event Action<BaseEatObjects> Eated;

    public float HealSize { get; private set; }

    private void Awake()
    {
        HealSize = UnityEngine.Random.Range(_minKitSize, _maxKitSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Healer frog))
            Eated?.Invoke(this);
    }
}
