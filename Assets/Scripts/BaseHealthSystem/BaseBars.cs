using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseBars : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private BaseHeroPoints _value;
    [SerializeField] private float _changeRate;

    private bool _running;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _running = true;
        _coroutine = StartCoroutine(ChangeSmoothBarView());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _running = false;
    }

    private IEnumerator ChangeSmoothBarView()
    {
        while (_running)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _value.Value / _value.MaxValue, _changeRate * Time.fixedDeltaTime);

            yield return null;
        }
    }
}
