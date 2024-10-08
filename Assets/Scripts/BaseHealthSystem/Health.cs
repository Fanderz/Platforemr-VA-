using System;

public class Health : BaseHealthMana
{
    public event Action<float> Changed;

    public override void IncreaseValue(float value)
    {
        base.IncreaseValue(value);

        Changed?.Invoke(Value);
    }

    public override void DecreaseValue(float value)
    {
        base.DecreaseValue(value);

        Changed?.Invoke(Value);
    }
}
