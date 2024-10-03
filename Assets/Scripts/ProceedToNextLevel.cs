using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ProceedToNextLevel : MonoBehaviour
{
    public string level2 = "Level2";
    public string level3 = "Level3";
    public string level4 = "Level4";
    public string level5 = "Level5";
    public string OutroStory = "OutroSceneStory";
    public string Minigame = "MiniGame";
    public void Proceedafterlvl1()
    {
        SceneManager.LoadScene(level2);
    }
    public void Proceedafterlvl2()
    {
        SceneManager.LoadScene(level3);
    }
    public void Proceedafterlvl3()
    {
        SceneManager.LoadScene(level4);
    }
    public void Proceedafterlvl4()
    {
        SceneManager.LoadScene(level5);
    }
    public void Proceedafterlvl5()
    {
        SceneManager.LoadScene(OutroStory);
    }
    
}
