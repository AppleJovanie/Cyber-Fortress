using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI roundsText;
    public SceneFader sceneFader;
    private string menuScene = "MainMenuNewScene";
    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
     
    }

    public void Retry()
    {

        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
       
    }
    public void Menu()
    {

        sceneFader.FadeTo(menuScene);
    }
  
}
