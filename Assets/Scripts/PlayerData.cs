using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int lives;
    public int rounds;
    public string currentLevel;

    // Add lists for nodes and enemies
    public List<NodeData> nodes = new List<NodeData>();
    public List<EnemyData> enemies = new List<EnemyData>();
}

[System.Serializable]
public class NodeData
{
    public SerializableVector3 position;
    public string turretName;
    public bool isUpgraded;
    public bool isFinalUpgraded;
}

[System.Serializable]
public class EnemyData
{
    public SerializableVector3 position;
    public string enemyName;
    public float health;
    public int waypointIndex;
}