using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float gameDuration = 60f; // Set your game time (e.g., 60 seconds)
    private float timeRemaining;
    public Text timerText; // Assign in Inspector for displaying time
    public Text gameOverText; // Assign in Inspector for "Game Over" message
    private bool isGameOver = false;
    public  ARTouchSpawn over;

    void Start()
    {
        timeRemaining = gameDuration;
        gameOverText.gameObject.SetActive(false); // Hide "Game Over" initially
    }

    void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        // Decrease remaining time
        timeRemaining -= Time.deltaTime;

        // Update UI
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();

        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true); // Show "Game Over" text
        timerText.text = "Time: 0"; // Set timer to 0
        over.gameover();
        StopAllSpiders(); // Implement this to stop spider behavior
    }

    void StopAllSpiders()
    {
        // Find all spider objects and disable their movement or other actions
        GameObject[] spiders = GameObject.FindGameObjectsWithTag("Spider");
        foreach (GameObject spider in spiders)
        {
            spider.GetComponent<Spider>().enabled = false;
        }
    }
}

