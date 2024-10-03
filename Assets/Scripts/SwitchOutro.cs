using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Import the UI namespace for working with buttons
using UnityEngine.SceneManagement;

public class SwitchOutro : MonoBehaviour
{
    public GameObject[] background;
    public Button nextButton;  // Reference to the Next button
    public string GoToMiniGame = "MiniGme";  // Name of the mini-game scene

    private int index;

    void Start()
    {
        // Initialize index and set the first background as active
        index = 0;
        SetActiveBackground();
        UpdateButtonState();  // Disable the Next button if necessary
    }

    public void Next()
    {
        // Check if we're at the last index
        if (index >= background.Length - 1)
        {
            // Proceed to the mini-game scene
            SceneManager.LoadScene(GoToMiniGame);
            return;  // Exit the method
        }

        // Increment the index
        index++;

        // Ensure it doesn't go out of bounds
        if (index >= background.Length)
        {
            index = background.Length - 1;  // Stay at the last index
            return;  // Exit the method
        }

        SetActiveBackground();
        UpdateButtonState();
    }

    public void Previous()
    {
        // Ensure that the index does not go below 0 (no loop)
        if (index > 0)
        {
            index--;
            SetActiveBackground();
            UpdateButtonState();
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

    // Method to enable/disable the Next button based on the index
    void UpdateButtonState()
    {
        // Disable and hide the Next button if we're at the last index
        if (index >= background.Length - 1)
        {
            nextButton.gameObject.SetActive(true);  // Hide and disable the Next button
        }
        else
        {
            nextButton.gameObject.SetActive(true);  // Show and enable the Next button
        }
    }
}
