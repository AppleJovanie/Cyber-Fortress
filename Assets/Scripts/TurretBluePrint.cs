using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject preFab;
    public int cost;
    public Vector3 positionOffset;
    public Vector3 upgradePositionOffset;
    public GameObject upgradedPrefab;
    public int upgradeCost;
    public GameObject finalUpgradePrefab; // Add this field
    public int finalUpgradeCost; // Add this field
}
