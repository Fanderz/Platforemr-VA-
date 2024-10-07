using System;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public float Value { get; private set; }

    public event Action<float> ManaChanged;

    public float MaxValue => _maxValue;

    private void Awake()
    {
        Value = _maxValue;
    }

    internal void IncreaseMana(float value)
    {
        if (Value + value <= _maxValue)
            Value += value;
        else
            Value = _maxValue;

        ManaChanged?.Invoke(Value);
    }

    internal void DecreaseMana(float value)
    {
        if (Value - value > 0)
            Value -= value;
        else
            Value = 0;

        ManaChanged?.Invoke(Value);
    }
}
