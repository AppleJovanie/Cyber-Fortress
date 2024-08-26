using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    private Node target;
    public GameObject UI;
    public Text upgradeCostText; // Reference to the Text component for upgrade cost
    public Button upgradeButton; // Reference to the upgrade button
    public Text sellAmountText; // Reference to the Text component for sell amount
    public Button sellButton; // Reference to the sell button

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();    
    }
    public void SetTarget(Node _target)
    {
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
    }

    public void Hide()
    {
        UI.SetActive(false);
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
