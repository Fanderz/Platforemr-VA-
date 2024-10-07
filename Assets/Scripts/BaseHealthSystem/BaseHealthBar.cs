using UnityEngine;

public class BaseHealthBar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void Awake()
    {
        Health.HealthChanged += ChangeHealthView;
    }

    public virtual void ChangeHealthView(float value)
    {
    }
}
