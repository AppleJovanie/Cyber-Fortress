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

        PlayerStats.Money -= turretToBuild.cost;

        Vector3 buildPosition = node.transform.position + turretToBuild.positionOffset;
        GameObject turret = (GameObject)Instantiate(turretToBuild.preFab, buildPosition, Quaternion.identity);
        node.turret = turret;

        GameObject effect = (GameObject)Instantiate(buildEffect, buildPosition, Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Turret Built! Money Left: " + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        Debug.Log("Turret selected: " + (turretToBuild != null ? turretToBuild.preFab.name : "None"));
    }

}
