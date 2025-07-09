using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.SceneManagement;

public class InGameStateManager : MonoBehaviour
{
    private float GameTime = 0f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameControllManager gameControllManager;
    private bool isGameStarted = true, isGameEnded = false;
    [SerializeField] private float ThreeStart, TwoStar, OneStar;

    [SerializeField] private GameObject gameOverPanel, gameClearedPanel;

    public void TriggerGameOver()
    {
        Debug.Log("GameOver");
        isGameEnded = true;
        gameOverPanel.SetActive(true);
        gameControllManager.RemoveAllControls();
    }
    public void TriggerGameCleared()
    {
        Debug.Log("Game Cleared.");
        isGameEnded = true;
        gameClearedPanel.SetActive(true);
        gameControllManager.RemoveAllControls();
    }
    public void TriggerStartGame()
    {
        Debug.Log("Game Started.");
    }
    public void GamePaused()
    {
        Time.timeScale = 0f;
        Debug.Log("Paused.");
    }
    public void GameResumed()
    {
        Time.timeScale = 1f;
        Debug.Log("Resumed.");
    }
    void Update()
    {
        if (isGameStarted && !isGameEnded)
        {
            GameTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(GameTime / 60f);
            int seconds = Mathf.FloorToInt(GameTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            // timerText.text = GameTime.ToString() ;
        }
    }
    public void RestartGame()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
