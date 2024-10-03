using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;

    void Start()
    {
        PlayerStats.Rounds = 0; // Reset rounds when starting a new game
    }

    public void Play()
    {
        Time.timeScale = 1f; // Ensure the game starts in a running state
        sceneFader.FadeTo("IntroSceneStory");
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            PlayerStats.Money = data.money;
            PlayerStats.Lives = data.lives;
            PlayerStats.Rounds = data.rounds;

            Time.timeScale = 1f; // Ensure the game is running
            sceneFader.FadeTo(data.currentLevel);
        }
    }
}
