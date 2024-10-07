using System;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public event Action<Fruit> Eated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out FrogEatingFruit frog))
            Eated?.Invoke(this);
    }

    public void Activate() =>
        SetActivity(true);

    public void Deactivate() =>
        SetActivity(false);

    private void SetActivity(bool value) =>
        gameObject.SetActive(value);
}
