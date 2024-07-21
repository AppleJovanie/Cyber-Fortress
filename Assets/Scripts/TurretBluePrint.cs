using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    //Upgrade 1
    public GameObject preFab;
    public int cost;
    public Vector3 positionOffset;

    //Upgrade 2
    
    public GameObject upgradedPrefab;
    public Vector3 upgradePositionOffset;
    public int upgradeCost;

    //FinalUpgrade
    public GameObject finalUpgradePrefab; // Add this field
    public int finalUpgradeCost; // Add this field
    public Vector3 finalUpgradePositionOffset;
}
