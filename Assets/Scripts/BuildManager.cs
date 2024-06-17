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

    public GameObject ByteSweeperPrefab;
    public GameObject SpyBotTurrePrefab;
    public GameObject VastaCannonPrefab;

    void Start()
    {
        turretToBuild = ByteSweeperPrefab;
       
    }

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
    public void SetTurretToBuild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
