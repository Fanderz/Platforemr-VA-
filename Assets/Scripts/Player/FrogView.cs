using UnityEngine;

public class FrogView : MonoBehaviour
{
    internal void Rotate(float direction)
    {
        if (direction > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, -180, 0);
    }
}
