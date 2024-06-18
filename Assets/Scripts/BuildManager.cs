using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build Manager");
            return;
        }
        instance = this;


    }
    public GameObject ByteSweeperPrefab; //Standard TurretPrefab
    public GameObject SpyBotTurrePrefab;
    public GameObject VastaCannonPrefab;


    //Build Effect Particle
    public GameObject buildEffect;

    private TurretBluePrint turretToBuild;
    public bool CanBuild
    {
        get { return turretToBuild != null; }
    }

    public bool HasMoney
    {
        get { return PlayerStats.Money >= turretToBuild.cost; }
    }

    public void BuildTurretOn(Node node)
    {
        if (turretToBuild == null)
        {
            Debug.LogError("No turret selected to build.");
            return;
        }

        //Check if the player has enough money
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not Enough Money");
            return;
        }

        if (turretToBuild.preFab == null)
        {
            Debug.LogError("Turret prefab is null!");
            return;
        }

        //if yes build it and minus the cost of the turret to the player money
        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.preFab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

       GameObject effect = (GameObject) Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret Built! Money Left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        Debug.Log("Turret selected: " + (turretToBuild != null ? turretToBuild.preFab.name : "None"));
    }

}
