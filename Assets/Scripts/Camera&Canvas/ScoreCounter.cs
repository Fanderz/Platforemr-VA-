using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private FrogEatingFruit _frog;

    public event Action<int> Changed;

    public int Value { get; private set; }

    private void Awake()
    {
        Value = 0;
    }

    private void OnEnable()
    {
        _frog.ScoreIncreased += OnScoreIncreased;
    }

    private void OnDisable()
    {
        _frog.ScoreIncreased -= OnScoreIncreased;
    }

    private void OnScoreIncreased()
    {
        Value++;

        Changed?.Invoke(Value);
    }
}
