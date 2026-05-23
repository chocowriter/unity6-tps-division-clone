using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Title,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoreManager;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        ChangeState(GameState.Title);
    }

    public void StartGame()
    {
        scoreManager.ResetScore();
        ChangeState(GameState.Playing);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (CurrentState)
        {
            case GameState.Title:
                Time.timeScale = 0f;
                uiManager.ShowTitle();
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                uiManager.ShowGame();
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                uiManager.ShowGameOver(scoreManager.GetFinalScore());
                break;
        }
    }
}