using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.SceneManagement;
using System.Collections;

public class InGameStateManager : MonoBehaviour
{
    private float GameTime = 0f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameControllManager gameControllManager;
    private bool isGameStarted = true, isGameEnded = false;
    [SerializeField] private float ThreeStar, TwoStar, OneStar;
    [SerializeField] private GameObject ThreeStarPanel, TwoStarPanel, OneStarPanel;

    [SerializeField] private GameObject gameOverPanel, gameClearedPanel;
     public float slowDownFactor ; // Slow motion speed
    public float slowDuration ;
    private SpeedingAnimiation speedingAnimation;

    void Start(){
        speedingAnimation=FindAnyObjectByType<SpeedingAnimiation>();
    }

    void Awake()
    {
        Application.targetFrameRate = 60; // Set target frame rate
    }

    public void TriggerGameOver()
    {
        Debug.Log("GameOver");
        isGameEnded = true;
        gameOverPanel.SetActive(true);
        gameControllManager.RemoveAllControls();
        speedingAnimation.EndAnimation();
    }
    public void TriggerGameCleared()
    {
        Debug.Log("Game Cleared.");
        isGameEnded = true;
        gameClearedPanel.SetActive(true);
        gameControllManager.RemoveAllControls();
        CalculateRatings();
        speedingAnimation.EndAnimation();
    }

    public void CalculateRatings()
    {
        if (GameTime <= ThreeStar)
        {
            ThreeStarPanel.SetActive(true);
        }
        else if (GameTime <= TwoStar)
        {
            TwoStarPanel.SetActive(true);
        }
        else
        {
            OneStarPanel.SetActive(true);
        }
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
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void TriggerSlowMotion()
    {
        StartCoroutine(SlowMotion());
    }

    IEnumerator SlowMotion()
{
    // wait new WaitForSecondsRealtime(0.5f); // Wait for 0.5 seconds in real time before starting slow motion
    Time.timeScale = slowDownFactor;
    Time.fixedDeltaTime = 0.02f * Time.timeScale;

    float elapsed = 0f;
    while (elapsed < slowDuration)
    {
        elapsed += Time.unscaledDeltaTime; // real-world time
        yield return null;
    }

    Time.timeScale = 1f;
    Time.fixedDeltaTime = 0.02f;
}

}
