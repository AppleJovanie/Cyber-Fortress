using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBluePrint byteSweeperTurret;
    public TurretBluePrint spyBotsTurret;
    public TurretBluePrint vastaCannonTurret;
    public TurretBluePrint WindowsDefenderTurret;

    BuildManager buildManager;

     void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        Debug.Log("ByteSweeper Turret Purchased");
        buildManager.SelectTurretToBuild(byteSweeperTurret);
    }

    public void SelectSpyBotTurret()
    {
        Debug.Log("SpyBot Turret Purchased");
        buildManager.SelectTurretToBuild(spyBotsTurret);
    }
    public void SelectVastaCannonTurret()
    {
        Debug.Log("Vasta Cannon Turret Purchased");
        buildManager.SelectTurretToBuild(vastaCannonTurret);
    }
}
