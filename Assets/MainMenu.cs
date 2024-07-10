using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level1 = "MainLevel";
   public void Play()
    {
        SceneManager.LoadScene(level1);
    }
    public void Quit()
    {
        Debug.Log("Quiting...");
        Application.Quit();
    }
}
