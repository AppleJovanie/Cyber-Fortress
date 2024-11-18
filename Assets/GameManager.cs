using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;
    public GameObject gameOverUI;
    public TextMeshProUGUI roundsText;
    public SceneFader sceneFader;
    private string menuScene = "MainMenuNewScene";


    void Start()
    {
        gameIsOver = false;
        Time.timeScale = 1.0f; // Ensure the game runs normally on start
    }

    void Update()
    {
        if (gameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsOver = true;

        // Update rounds text
        if (roundsText != null)
            roundsText.text = PlayerStats.Rounds.ToString();

        // Show the Game Over UI
        gameOverUI.SetActive(true);

        // Pause the game logic
        Time.timeScale = 1.0f;

        Debug.Log("Game Over");
    }

    public void Retry()
    {
        // Reset the game state and resume time
        gameIsOver = false;
        Time.timeScale = 1.0f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void Menu()
    {
        AudioManager.Instance.StopMusic();

        // Resume game time before loading the main menu
        Time.timeScale = 1.0f;
        sceneFader.FadeTo(menuScene);
    }
}
