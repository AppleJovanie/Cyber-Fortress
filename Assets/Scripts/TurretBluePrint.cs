using UnityEngine;

[System.Serializable]
public class TurretBluePrint
{
    public GameObject preFab;
    public int cost;
    public Vector3 positionOffset; // Add this field to define custom offsets for each turret
}
