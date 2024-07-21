using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab; // The enemy prefab to spawn
    public int count; // The number of enemies in this wave
    public float spawnInterval; // Special rate for wave
}
