using System;
using UnityEngine;

public class BaseHeroPoints : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public float Value { get; protected set; }

    public virtual event Action<float> Changed;

    public float MaxValue => _maxValue;

    private void Awake()
    {
        Value = _maxValue;
    }

    public virtual void IncreaseValue(float value)
    {
        if (Value + value <= _maxValue)
            Value += value;
        else
            Value = _maxValue;

        Changed?.Invoke(Value);
    }

    public virtual void DecreaseValue(float value)
    {
        if (Value - value > 0)
            Value -= value;
        else
            Value = 0;

        Changed?.Invoke(Value);
    }
}
