using System;
using UnityEngine;

public class Healer : MonoBehaviour
{
    public event Action<float> Healing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out AidKit kit))
            Healing?.Invoke(kit.HealSize);
    }
}
