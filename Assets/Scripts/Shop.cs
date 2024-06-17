using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

     void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.ByteSweeperPrefab);
    }

    public void PurchaseSpyBotTurret()
    {
        Debug.Log("SpyBot Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.SpyBotTurrePrefab);
    }
    public void PurchaseVastaCannonTurret()
    {
        Debug.Log("Vasta Cannon Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.VastaCannonPrefab);
    }
}
