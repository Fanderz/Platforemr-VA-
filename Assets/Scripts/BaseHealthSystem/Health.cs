using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public float Value { get; private set; }

    public event Action<float> HealthChanged;

    public float MaxValue => _maxValue;

    private void Awake()
    {
        Value = _maxValue;
    }

    internal void IncreaseHealth(float value)
    {
        if (Value + value <= _maxValue)
            Value += value;
        else
            Value = _maxValue;

        HealthChanged?.Invoke(Value);
    }

    internal void DecreaseHealth(float value)
    {
        if (Value - value > 0)
            Value -= value;
        else
            Destroy(gameObject);

        HealthChanged?.Invoke(Value);
    }
}
