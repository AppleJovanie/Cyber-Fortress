using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switch : MonoBehaviour
{
    public GameObject[] background;
    public string ProceedToScene = "Level1";
    private int index;

    void Start()
    {
        // Load the index from PlayerPrefs, default to 0 if not present
        //index = PlayerPrefs.GetInt("index", 0);
        SetActiveBackground();
    }

    public void Next()
    {
        index++;

        // Check if index is equal to the length of the background array (last index)
        if (index >= background.Length)
        {
            // If it is, load the next scene
            SceneManager.LoadScene(ProceedToScene);
            return; // Exit the method to prevent further execution
        }

        SetActiveBackground();
    }

    public void Previous()
    {
        // Ensure that the index does not go below 0 (no loop)
        if (index > 0)
        {
            index--;
            SetActiveBackground();
        }
    }

   
    // Method to activate the background based on the current index
    void SetActiveBackground()
    {
        // Ensure only the current index is active
        for (int i = 0; i < background.Length; i++)
        {
            if (background[i] != null)
            {
                background[i].SetActive(i == index);  // Set active only if it's the correct index
            }
        }
    }

}

