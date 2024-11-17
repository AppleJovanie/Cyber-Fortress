using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlmanacPanel : MonoBehaviour
{
    public GameObject AlmanacP;  // Main tutorial panel
    public GameObject[] panels;  // Array of individual tutorial slides
    public Button nextButton;    // Button to go to the next slide
    public Button previousButton; // Button to go to the previous slide
    public Button closeButton;   // Button to close the tutorial
    public AudioSource backgroundMusic; // Reference to the background music AudioSource

    private int currentPanelIndex = 0;

    void Start()
    {
        AlmanacP.SetActive(false);  // Initially, the tutorial is hidden
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
            ToggleAlmanac();
        }
    }

    public void TogglePanel()
    {
        ToggleAlmanac();
        Debug.Log("Pressed Toggle Panel");
    }

    public void ToggleAlmanac()
    {
        if (AlmanacP.activeSelf)
        {
            // Unpause, hide the panel, and resume music
            AlmanacP.SetActive(false);
            panels[currentPanelIndex].SetActive(false);      // Hide current slide
            nextButton.gameObject.SetActive(false);          // Hide next button
            previousButton.gameObject.SetActive(false);      // Hide previous button
            closeButton.gameObject.SetActive(false);         // Hide close button
            Time.timeScale = 1f;

            // Resume the background music
            if (backgroundMusic != null)
                backgroundMusic.Play();
        }
        else
        {
            // Pause the game, show the panel, and stop the music
            AlmanacP.SetActive(true);
            panels[currentPanelIndex].SetActive(true);       // Show current slide
            nextButton.gameObject.SetActive(true);           // Show next button
            previousButton.gameObject.SetActive(true);       // Show previous button
            closeButton.gameObject.SetActive(true);          // Show close button
            Time.timeScale = 0f;

            // Pause the background music
            if (backgroundMusic != null)
                backgroundMusic.Pause();

            // Ensure buttons are updated based on the current panel
            UpdatePanel();
        }
    }

    public void CloseHowToPlay()
    {
        // Hide the entire how-to-play panel
        AlmanacP.SetActive(false);

        // Hide the currently active tutorial slide
        panels[currentPanelIndex].SetActive(false);

        // Hide all buttons
        nextButton.gameObject.SetActive(false);
        previousButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);

        // Resume game time
        Time.timeScale = 1f;

        // Resume the background music
        if (backgroundMusic != null)
            backgroundMusic.Play();
    }
}
