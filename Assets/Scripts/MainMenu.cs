using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level1 = "MainLevel";
    public SceneFader sceneFader;

    public void Play()
    {
        sceneFader.FadeTo(level1);
    }

    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void Load()
    {
        //PlayerData data = SaveSystem.LoadPlayer();
        //if (data != null)
        //{
        //    GameManager.instance.score = data.score;
        //    GameManager.instance.health = data.health;
        //    GameManager.instance.player.transform.position = data.playerPosition;
        //    WaveSpawner.currentWaveIndex = data.currentWave;
        //    PlayerStats.Money = data.money;
        //    PlayerStats.Lives = data.lives;
        //    PlayerStats.Rounds = data.rounds;

        //    sceneFader.FadeTo(level1);
        //}
    }
}
