using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _score;
    [SerializeField] private TextMeshProUGUI _text;

    private void Awake()
    {
        _text.text = _score.Score.ToString();
    }

    private void OnEnable()
    {
        _score.Changed += DisplayScore;
    }

    private void OnDisable()
    {
        _score.Changed -= DisplayScore;
    }

    private void DisplayScore(int score)
    {
        _text.text = score.ToString();
    }
}
