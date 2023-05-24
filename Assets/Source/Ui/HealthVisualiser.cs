using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthVisualiser : MonoBehaviour
{
    [SerializeField] private KillablePresenter _creature;
    [SerializeField] private float _maxDeltaValue;

    private Slider _healthBar;
    private Coroutine _coroutine;
    private bool _isCoroutineOn = false;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _creature.Inited += OnInited;
    }

    private void OnDisable()
    {
        _creature.Inited -= OnInited;
        _creature.Model.HealthChanged -= OnHealthChanged;
    }

    private void OnInited()
    {
        _creature.Model.HealthChanged += OnHealthChanged;
        _healthBar.maxValue = _creature.Model.Health;
        _healthBar.value = _healthBar.maxValue;
        _healthBar.minValue = 0;
    }

    private void OnHealthChanged()
    {
        StartValueChanging();
    }

    private void StartValueChanging()
    {
        if (_isCoroutineOn)
        {
            _isCoroutineOn = false;
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeValue(_creature.Model.Health));
    }

    private IEnumerator ChangeValue(int newValue)
    {
        _isCoroutineOn = true;

        while (_healthBar.value != newValue)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, newValue, _maxDeltaValue);
            yield return null;
        }

        _isCoroutineOn = false;
    }
}
