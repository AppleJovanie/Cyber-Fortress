using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughMoneyColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    [HideInInspector]
    public bool isFinalUpgraded = false;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        if (isFinalUpgraded)
        {
            return transform.position + turretBlueprint.finalUpgradePositionOffset;
        }
        return transform.position + (isUpgraded ? turretBlueprint.upgradePositionOffset : turretBlueprint.positionOffset);
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
       
    }

    void BuildTurret(TurretBluePrint blueprint)
    {
        if (blueprint == null)
        {
            Debug.LogError("No turret selected to build.");
            return;
        }

        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        if (blueprint.preFab == null)
        {
            Debug.LogError("Turret prefab is null!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        Vector3 buildPosition = transform.position + blueprint.positionOffset;
        GameObject _turret = (GameObject)Instantiate(blueprint.preFab, buildPosition, Quaternion.identity);
        audioManager.PlaySfx(audioManager.BuildingEffect);
        turret = _turret;

        turretBlueprint = blueprint;
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, buildPosition, Quaternion.identity);
       
        Destroy(effect, 5f);

        Debug.Log("Turret Built! Money Left: " + PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        if (isFinalUpgraded)
        {
            return;
        }

        if (isUpgraded)
        {
            FinalUpgradeTurret();
            return;
        }

        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not Enough Money To Upgrade");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        Vector3 upgradeBuildPosition = transform.position + turretBlueprint.upgradePositionOffset;
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, upgradeBuildPosition, Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, upgradeBuildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        Debug.Log("Turret Upgraded! " + PlayerStats.Money);
    }

    public void FinalUpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.finalUpgradeCost)
        {
            Debug.Log("Not Enough Money For Final Upgrade");
            return;
        }

        PlayerStats.Money -= turretBlueprint.finalUpgradeCost;

        Destroy(turret);

        Vector3 finalUpgradeBuildPosition = transform.position + turretBlueprint.finalUpgradePositionOffset;
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.finalUpgradePrefab, finalUpgradeBuildPosition, Quaternion.identity);
        turret = _turret;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, finalUpgradeBuildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        isFinalUpgraded = true;

        Debug.Log("Turret Final Upgraded! " + PlayerStats.Money);
    }

    public void SellTurret()
    {
        int sellAmount = turretBlueprint.cost / 2;
        if (isUpgraded)
        {
            sellAmount += turretBlueprint.upgradeCost / 2;
        }
        if (isFinalUpgraded)
        {
            sellAmount += turretBlueprint.finalUpgradeCost / 2;
        }

        PlayerStats.Money += sellAmount;

        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
        isFinalUpgraded = false;

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, transform.position, Quaternion.identity);
       
        Destroy(effect, 5f);
       

        Debug.Log("Turret Sold! Money Received: " + sellAmount);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
