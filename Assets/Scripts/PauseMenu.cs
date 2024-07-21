using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUi;
    public GameObject audioMenuUi; // Reference to the audio settings panel
    public SceneFader sceneFader;
    public string menuScene = "MainMenu";

    private GameObject currentPanel; // Track the current panel

    void Start()
    {
        currentPanel = pauseMenuUi;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        pauseMenuUi.SetActive(!pauseMenuUi.activeSelf);

        if (pauseMenuUi.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(menuScene);
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Save()
    {
        //PlayerData data = new PlayerData
        //{
        //    score = GameManager.instance.score,
        //    health = GameManager.instance.health,
        //    playerPosition = GameManager.instance.player.transform.position,
        //    currentWave = WaveSpawner.currentWaveIndex,
        //    money = PlayerStats.Money,
        //    lives = PlayerStats.Lives,
        //    rounds = PlayerStats.Rounds
        //};

        //SaveSystem.SavePlayer(data);
    }

    public void OpenAudioMenu()
    {
        SwitchPanel(audioMenuUi);
    }

    public void Back()
    {
        SwitchPanel(pauseMenuUi);
    }

    private void SwitchPanel(GameObject newPanel)
    {
        currentPanel.SetActive(false);
        newPanel.SetActive(true);
        currentPanel = newPanel;
    }
}
