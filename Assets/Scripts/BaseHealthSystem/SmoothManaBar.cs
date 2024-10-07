using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothManaBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Mana _health;
    [SerializeField] private float _changeRate;

    private bool _running;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _running = true;
        _coroutine = StartCoroutine(ChangeSmoothManaView());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
        _running = false;
    }

    private IEnumerator ChangeSmoothManaView()
    {
        while (_running)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _health.Value / _health.MaxValue, _changeRate * Time.fixedDeltaTime);

            yield return null;
        }
    }
}
