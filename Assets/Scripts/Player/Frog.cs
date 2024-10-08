using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Frog : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Fighter _fighter;
    [SerializeField] private Healer _healer;

    private GroundDetector _groundDetector;
    private InputReader _inputReader;

    public event Action<bool, float> AnimatorParameterChanged;
    public event Action<float> Moving;
    public event Action Jumping;

    private void Awake()
    {
        _fighter.TakingDamage += _health.DecreaseValue;
        _healer.Healing += _health.IncreaseValue;
        _groundDetector = GetComponentInChildren<GroundDetector>();
        _inputReader = GetComponentInChildren<InputReader>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
            Moving?.Invoke(_inputReader.Direction);

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            Jumping?.Invoke();

        if (_health.Value <= 0)
            SceneManager.LoadScene("SampleScene");
    }

    private void Update()
    {
        AnimatorParameterChanged?.Invoke(!_groundDetector.IsGround, Mathf.Abs(_inputReader.Direction));
    }
}
