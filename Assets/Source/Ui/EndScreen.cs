using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _startNewSessionButton;

    public event UnityAction RestartScene;

    private void OnEnable()
    {
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _startNewSessionButton.onClick.AddListener(OnStartNewSessionButtonClick);
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _startNewSessionButton.onClick.RemoveListener(OnStartNewSessionButtonClick);
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }

    private void OnStartNewSessionButtonClick()
    {
        RestartScene?.Invoke();
    }
}
