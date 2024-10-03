using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelNavigation : MonoBehaviour
{
    public GameObject[] panels;  // Array to store all your panels
    private int currentPanelIndex = 0;  // Keeps track of the current panel

    void Start()
    {
        // Initially, deactivate all panels except the first one
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }
    }

    // Method to show the next panel
    public void ShowNextPanel()
    {
        if (currentPanelIndex < panels.Length - 1)
        {
            panels[currentPanelIndex].SetActive(false);  // Hide the current panel
            currentPanelIndex++;  // Move to the next panel
            panels[currentPanelIndex].SetActive(true);  // Show the next panel
        }
    }

    // Method to show the previous panel
    public void ShowPreviousPanel()
    {
        if (currentPanelIndex > 0)
        {
            panels[currentPanelIndex].SetActive(false);  // Hide the current panel
            currentPanelIndex--;  // Move to the previous panel
            panels[currentPanelIndex].SetActive(true);  // Show the previous panel
        }
    }
}
