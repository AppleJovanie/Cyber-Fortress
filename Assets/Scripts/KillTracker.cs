using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTracker : MonoBehaviour
{
    public static KillTracker instance;
    public int kills = 0;
    public int cubitzPerBatch = 200;
    public int killsPerBatch = 10;
    private int totalEnemies; // Store the total number of enemies across all waves

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one KillTracker in scene!");
            return;
        }
        instance = this;
    }

    public void SetTotalEnemies(int total)
    {
        totalEnemies = total;
    }

    public bool HasCubitzToCollect()
    {
        return kills >= killsPerBatch;
    }

    public void CollectCubitz()
    {
        if (kills >= killsPerBatch)
        {
            PlayerStats.Money += cubitzPerBatch;
            kills -= killsPerBatch;
        }
    }
    public bool HasKilledAllEnemies()
    {
        // Check if kills are equal to the total enemies
        return kills >= totalEnemies;
    }
}
