using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBarView : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Health _health;
    [SerializeField] private float _changeRate;

    private bool _running;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _running = true;
        _coroutine = StartCoroutine(ChangeSmoothHealthView());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _running = false;
    }

    private IEnumerator ChangeSmoothHealthView()
    {
        while (_running)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _health.Value / _health.MaxValue, _changeRate * Time.fixedDeltaTime);

            yield return null;
        }
    }
}
