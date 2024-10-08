using System;
using UnityEngine;

public class BaseEatObjects : MonoBehaviour
{
    public virtual event Action<BaseEatObjects> Eated;

    public virtual void Activate() =>
    SetActivity(true);

    public virtual void Deactivate() =>
        SetActivity(false);

    protected void SetActivity(bool value) =>
        gameObject.SetActive(value);
}
