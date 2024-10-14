public class Health : BaseHeroPoints
{
    private void OnEnable()
    {
        Changed += OnValueChanged;
    }

    private void OnDisable()
    {
        Changed -= OnValueChanged;
    }

    private void OnValueChanged(float value)
    {
        if (value == 0)
            Destroy(gameObject);
    }
}
