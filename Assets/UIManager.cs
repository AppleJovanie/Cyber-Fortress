using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private GameObject activeUI = null; // Reference to the currently active UI

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowCubitzTowerUI(CubtizTowerNode target)
    {
        // Hide the active UI if it's not the current one
        if (activeUI != null && activeUI != target.gameObject)
        {
            activeUI.SetActive(false);
        }

        // Show the CubitzTowerUI and update the active UI reference
        target.gameObject.SetActive(true);
        activeUI = target.gameObject;
    }

    public void ShowNodeUI(Node target)
    {
        // Hide the active UI if it's not the current one
        if (activeUI != null && activeUI != target.gameObject)
        {
            activeUI.SetActive(false);
        }

        // Show the NodeUI and update the active UI reference
        target.gameObject.SetActive(true);
        activeUI = target.gameObject;
    }

    public void HideActiveUI()
    {
        // Hide the currently active UI
        if (activeUI != null)
        {
            activeUI.SetActive(false);
            activeUI = null;
        }
    }
}
