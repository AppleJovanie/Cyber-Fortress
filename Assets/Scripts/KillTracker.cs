using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTracker : MonoBehaviour
{
    public static KillTracker instance;
    public int kills = 0;
    public int cubitzPerBatch = 200;
    public int killsPerBatch = 10;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one KillTracker in scene!");
            return;
        }
        instance = this;
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
}
