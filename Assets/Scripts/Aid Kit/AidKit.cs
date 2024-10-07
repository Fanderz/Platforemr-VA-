using UnityEngine;
using System;

public class AidKit : MonoBehaviour
{
    [SerializeField] private float _minKitSize;
    [SerializeField] private float _maxKitSize;

    public event Action<AidKit> Heal;

    public float HealSize { get; private set; }

    private void Awake()
    {
        HealSize = UnityEngine.Random.Range(_minKitSize,_maxKitSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Healer frog))
            Heal?.Invoke(this);
    }

    public void Activate() =>
        SetActivity(true);

    public void Deactivate() =>
        SetActivity(false);

    private void SetActivity(bool value) =>
        gameObject.SetActive(value);
}
