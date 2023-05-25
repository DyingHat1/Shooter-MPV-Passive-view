using UnityEngine;
using UnityEngine.Events;

public abstract class Presenter : MonoBehaviour
{
    private Transformable _model;

    public Transformable Model => _model;

    public event UnityAction Inited;

    public void Init(Transformable model)
    {
        _model = model;
        enabled = true;
        Subscribe();
        OnMoved();
        OnRotated();
        Inited?.Invoke();
    }

    private void Subscribe()
    {
        _model.Moved += OnMoved;
        _model.Rotated += OnRotated;
        _model.Destroyed += OnDestroyed;
    }

    private void OnDisable()
    {
        _model.Moved -= OnMoved;
        _model.Rotated -= OnRotated;
        _model.Destroyed -= OnDestroyed;
    }

    private void OnMoved()
    {
        transform.position = _model.Position;
    }

    private void OnRotated()
    {
        transform.rotation = Quaternion.Euler(0, 0, _model.Rotation);
    }

    private void OnDestroyed()
    {
        Destroy(gameObject);
    }
}
