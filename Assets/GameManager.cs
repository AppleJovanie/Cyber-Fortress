using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    public static bool gameIsOver;
    public GameObject gameOverUI;
    // Update is called once per frame

    void Start()
    {
        gameIsOver = false;  
    }
    void Update()
    {
        if (gameIsOver)
            return;

 
        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    void EndGame()
    {
        gameIsOver = true;

        Time.timeScale = 0f;


        gameOverUI.SetActive(true);
    }
}
