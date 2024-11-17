using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
  
    public float fadeDuration = 1f; // Local fade duration in MainMenu

    void Start()
    {
        PlayerStats.Rounds = 0; // Reset rounds when starting a new game
       
    }


    public void Play()
    {
        Time.timeScale = 1f; // Ensure the game starts in a running state
        StartCoroutine(StartGameAfterFade());
     // AudioManager.Instance.PlayBackgroundMusic();
    }

    private IEnumerator StartGameAfterFade()
    {
        // Start fading to the specified scene
        sceneFader.FadeTo("IntroSceneStory");

        // Wait for the fade to complete, using the local fadeDuration
        yield return new WaitForSeconds(fadeDuration);

               
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
         //AudioManager.Instance.PlayBackgroundMusic();
        }
    }
}
