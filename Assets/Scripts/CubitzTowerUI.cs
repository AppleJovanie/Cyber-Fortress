using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubitzTowerUI : MonoBehaviour
{
    public GameObject ui;
    public Text cubitzText; // Add a Text UI component to display the Cubitz available
    AudioManager audioManager;

    private CubtizTowerNode towerTarget;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SetTarget(CubtizTowerNode _towerTarget)
    {
        this.towerTarget = _towerTarget;
        transform.position = towerTarget.transform.position; // Position the UI accordingly
        ui.SetActive(true); // Show the UI

        UpdateCubitzText();
    }

    public void Hide()
    {
        ui.SetActive(false); // Hide the UI
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
