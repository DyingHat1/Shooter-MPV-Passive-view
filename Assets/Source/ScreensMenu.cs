using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreensMenu : MonoBehaviour
{
    [SerializeField] private EndScreen _loseScreen;
    [SerializeField] private EndScreen _winScreen;

    public void OpenWinScreen()
    {
        Time.timeScale = 0;
        _winScreen.gameObject.SetActive(true);
    }

    public void OpenLoseScreen()
    {
        Time.timeScale = 0;
        _loseScreen.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _loseScreen.RestartScene += OnRestartScene;
        _winScreen.RestartScene += OnRestartScene;
    }

    private void OnDisable()
    {
        _loseScreen.RestartScene -= OnRestartScene;
        _winScreen.RestartScene -= OnRestartScene;
    }

    private void OnRestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
