using UnityEngine;

public class BaseHealthMana : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public float Value { get; protected set; }

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
    }

    public virtual void DecreaseValue(float value)
    {
        if (Value - value > 0)
            Value -= value;
        else
            Destroy(gameObject);
    }
}
