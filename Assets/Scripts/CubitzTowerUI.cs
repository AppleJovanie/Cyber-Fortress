using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubitzTowerUI : MonoBehaviour
{
    public GameObject ui;
    public Text cubitzText; // Add a Text UI component to display the Cubitz available

    private CubtizTowerNode towerTarget;

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
        KillTracker.instance.CollectCubitz();
        UpdateCubitzText();
        
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
