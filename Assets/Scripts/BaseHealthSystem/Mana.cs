using System;

public class Mana : BaseHealthMana
{
    public event Action<float> Changed;

    public override void IncreaseValue(float value)
    {
        base.IncreaseValue(value);

        Changed?.Invoke(Value);
    }

    public override void DecreaseValue(float value)
    {
        if (Value - value > 0)
            Value -= value;
        else
            Value = 0;

        Changed?.Invoke(Value);
    }
}
