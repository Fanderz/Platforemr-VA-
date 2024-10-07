using UnityEngine;

public class DogView : MonoBehaviour
{
    private float _lastPosition;

    internal void Rotate(float direction)
    {
        if (direction < _lastPosition)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, -180, 0);

        _lastPosition = direction;
    }
}
