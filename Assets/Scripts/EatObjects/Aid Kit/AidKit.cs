using UnityEngine;
using System;

public class AidKit : BaseEatObjects
{
    [SerializeField] private float _minKitSize;
    [SerializeField] private float _maxKitSize;

    public float HealSize { get; private set; }

    private void Awake()
    {
        HealSize = UnityEngine.Random.Range(_minKitSize, _maxKitSize);
    }
}
