using System;
using UnityEngine;

public class BaseHeroPoints : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public virtual event Action<float> Changed;

    public float Value { get; protected set; }

    public float MaxValue => _maxValue;

    private void Awake()
    {
        Value = _maxValue;
    }

    public void IncreaseValue(float value)
    {
        Value = Mathf.Min(Value + value, _maxValue);

        Changed?.Invoke(Value);
    }

    public void DecreaseValue(float value)
    {
        Value = Mathf.Max(Value - value, 0);

        Changed?.Invoke(Value);
    }
}
