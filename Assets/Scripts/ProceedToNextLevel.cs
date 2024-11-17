using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ProceedToNextLevel : MonoBehaviour
{
    public int level2 = 3;
    public int level3 = 5;
    public int level4 = 7;
    public int level5 = 9;
    public int OutroStory = 12;

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
