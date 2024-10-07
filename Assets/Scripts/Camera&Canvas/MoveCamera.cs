using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Frog _frog;

    private Vector3 _offset;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        _offset = transform.position - _frog.transform.position;
    }

    private void LateUpdate()
    {
        _camera.transform.position = new Vector3(_frog.transform.position.x + _offset.x, _camera.transform.position.y, _camera.transform.position.z);
    }
}
