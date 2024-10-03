using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject UI;
    public GameObject cubitzUI; // Add reference to the Cubitz UI
    public Text upgradeCostText;
    public Button upgradeButton;
    public Text sellAmountText;
    public Button sellButton;

    AudioManager audioManager;
    private static GameObject activeUI = null;  // Static reference to the active UI

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void SetTarget(Node _target)
    {
        // Hide the currently active UI if it's not this one
        if (activeUI != null && activeUI != UI)
        {
            activeUI.SetActive(false);
        }

        // Hide Cubitz UI if it is active
        if (cubitzUI.activeSelf)
        {
            cubitzUI.SetActive(false);
        }

        this.target = _target;
        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCostText.text = "Upgrade: C" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else if (!target.isFinalUpgraded)
        {
            upgradeCostText.text = "Upgrade: C" + target.turretBlueprint.finalUpgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCostText.text = "MAX";
            upgradeButton.interactable = false;
        }

        int sellAmount = target.turretBlueprint.cost / 2;
        if (target.isUpgraded)
        {
            sellAmount += target.turretBlueprint.upgradeCost / 2;
        }
        if (target.isFinalUpgraded)
        {
            sellAmount += target.turretBlueprint.finalUpgradeCost / 2;
        }
        sellAmountText.text = "Sell: C" + sellAmount;
        sellButton.interactable = true;

        UI.SetActive(true);
        activeUI = UI; // Set this UI as the active UI
    }

    public void Hide()
    {
        UI.SetActive(false);
        if (activeUI == UI)
        {
            activeUI = null; // Clear the active UI reference if this is the one being hidden
        }
    }

    public void Upgrade()
    {
        if (!target.isUpgraded)
        {
            audioManager.PlaySfx(audioManager.UpgradeEffect);
            target.UpgradeTurret();
        }
        else if (!target.isFinalUpgraded)
        {
            audioManager.PlaySfx(audioManager.UpgradeEffect);
            target.FinalUpgradeTurret();
        }
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        audioManager.PlaySfx(audioManager.SellEffect);
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
