using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubitzTowerUI : MonoBehaviour
{
    public GameObject ui;
    public Text cubitzText;
    public GameObject nodeUI; // Add reference to the Node UI
    AudioManager audioManager;
    private CubtizTowerNode towerTarget;
    private static GameObject activeUI = null; // Static reference to the active UI

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SetTarget(CubtizTowerNode _towerTarget)
    {
        // Hide the currently active UI if it's not this one
        if (activeUI != null && activeUI != ui)
        {
            activeUI.SetActive(false);
        }

        // Hide Node UI if it is active
        if (nodeUI.activeSelf)
        {
            nodeUI.SetActive(false);
        }

        this.towerTarget = _towerTarget;
        transform.position = towerTarget.transform.position; // Position the UI accordingly
        ui.SetActive(true); // Show the UI
        activeUI = ui; // Set this UI as the active UI

        UpdateCubitzText();
    }

    public void Hide()
    {
        ui.SetActive(false); // Hide the UI
        if (activeUI == ui)
        {
            activeUI = null; // Clear the active UI reference if this is the one being hidden
        }
    }

    public void Collect()
    {
        if (KillTracker.instance.HasCubitzToCollect())
        {
            KillTracker.instance.CollectCubitz();
            audioManager.PlaySfx(audioManager.SellEffect); // Play the collect sound effect
            UpdateCubitzText();
        }
        else
        {
            Debug.Log("No Cubitz to collect");
        }
    }

    void UpdateCubitzText()
    {
        if (KillTracker.instance.HasCubitzToCollect())
        {
            cubitzText.text = "Collect";
        }
        else
        {
            cubitzText.text = "No Cubitz to collect";
        }
    }
}
