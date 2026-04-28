using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameOverEvent.OnGameOver += ShowGameOver;
    }

    private void OnDisable()
    {
        GameOverEvent.OnGameOver -= ShowGameOver;
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        SoundManager.PlayMusic(MusicType.GAME_OVER);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}