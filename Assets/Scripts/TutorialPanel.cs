using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    public GameObject howToPlayPanel;  // Main tutorial panel
    public GameObject[] panels;        // Array of individual tutorial slides
    public Button nextButton;          // Button to go to the next slide
    public Button previousButton;      // Button to go to the previous slide
    public Button closeButton;         // Button to close the tutorial
    public GameObject MainMenuPanel;

    private int currentPanelIndex = 0;

    void Start()
    {
        howToPlayPanel.SetActive(false);  // Initially, the tutorial is shown
     
        UpdatePanel();
    }

    public void ShowNextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            currentPanelIndex++;
            UpdatePanel();
        }
    }

    public void ShowPreviousPanel()
    {
        if (currentPanelIndex > 0)
        {
            currentPanelIndex--;
            UpdatePanel();
        }
    }

    private void UpdatePanel()
    {
        // Activate the current panel and deactivate others
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == currentPanelIndex);
        }

        // Disable the Next button if at the last panel
        nextButton.interactable = currentPanelIndex < panels.Length - 1;

        // Disable the Previous button if at the first panel
        previousButton.interactable = currentPanelIndex > 0;
    }

    void Update()
    {
        // Check for input to toggle the tutorial panel when 'T' is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleHowToPlay();
        }
    }
    public void TogglePanel()
    {
        ToggleHowToPlay();
        Debug.Log("Pressed Toggle Panel");
    }
   

    public void ToggleHowToPlay()
    {
        if (howToPlayPanel.activeSelf)
        {
            // Unpause and hide the panel
            howToPlayPanel.SetActive(false);
            panels[currentPanelIndex].SetActive(false);      // Hide current slide
            nextButton.gameObject.SetActive(false);          // Hide next button
            previousButton.gameObject.SetActive(false);      // Hide previous button
            closeButton.gameObject.SetActive(false);         // Hide close button
            Time.timeScale = 1f;
        }
        else
        {
            // Pause the game and show the panel
            howToPlayPanel.SetActive(true);
            panels[currentPanelIndex].SetActive(true);       // Show current slide
            nextButton.gameObject.SetActive(true);           // Show next button
            previousButton.gameObject.SetActive(true);       // Show previous button
            closeButton.gameObject.SetActive(true);          // Show close button
            Time.timeScale = 0f;

            // Ensure buttons are updated based on the current panel
            UpdatePanel();
        }
    }

    public void CloseHowToPlay()
    {
        // Hide the entire how-to-play panel
        howToPlayPanel.SetActive(false);

        // Hide the currently active tutorial slide
        panels[currentPanelIndex].SetActive(false);
        MainMenuPanel.SetActive(true);

        // Hide all buttons
        nextButton.gameObject.SetActive(false);
        previousButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);

        // Resume game time
        Time.timeScale = 1f;
    }
}
