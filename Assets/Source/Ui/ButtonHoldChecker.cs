using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHoldChecker : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private bool _isHolding;

    public event UnityAction Hold;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHolding = false;
    }

    private void Start()
    {
        _isHolding = false;
    }

    private void Update()
    {
        if (_isHolding)
            Hold?.Invoke();
    }
}
