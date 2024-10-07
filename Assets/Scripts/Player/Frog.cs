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

    public event Action<bool, float> OnAnimatorParameterChange;
    public event Action<float> OnFrogMove;
    public event Action OnFrogJump;

    private void Awake()
    {
        _fighter.TakingDamage += _health.DecreaseHealth;
        _healer.Healing += _health.IncreaseHealth;
        _groundDetector = GetComponentInChildren<GroundDetector>();
        _inputReader = GetComponentInChildren<InputReader>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
            OnFrogMove?.Invoke(_inputReader.Direction);

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            OnFrogJump?.Invoke();

        if (_health.Value <= 0)
            SceneManager.LoadScene("SampleScene");
    }

    private void Update()
    {
        OnAnimatorParameterChange?.Invoke(!_groundDetector.IsGround, Mathf.Abs(_inputReader.Direction));
    }
}
