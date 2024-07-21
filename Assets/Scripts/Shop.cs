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
        LogUpgradePosition(byteSweeperTurret);
    }

    public void SelectSpyBotTurret()
    {
        Debug.Log("SpyBot Turret Purchased");
        buildManager.SelectTurretToBuild(spyBotsTurret);
        LogUpgradePosition(spyBotsTurret);
    }

    public void SelectVastaCannonTurret()
    {
        Debug.Log("Vasta Cannon Turret Purchased");
        buildManager.SelectTurretToBuild(vastaCannonTurret);
        LogUpgradePosition(vastaCannonTurret);
    }

    public void SelectWindowsDefenderTurret()
    {
        Debug.Log("Windows Defender Turret Purchased");
        buildManager.SelectTurretToBuild(WindowsDefenderTurret);
        LogUpgradePosition(WindowsDefenderTurret);
    }

    private void LogUpgradePosition(TurretBluePrint turret)
    {
        if (turret != null)
        {
            Debug.Log("Position Offset for " + turret.preFab.name + ": " + turret.positionOffset);
            if (turret.upgradedPrefab != null)
            {
                Debug.Log("Upgrade Position Offset for " + turret.upgradedPrefab.name + ": " + turret.upgradePositionOffset);
            }
            if (turret.finalUpgradePrefab != null)
            {
                Debug.Log("Final Upgrade Position Offset for " + turret.finalUpgradePrefab.name + ": " + turret.finalUpgradePositionOffset);
            }
        }
    }
}
