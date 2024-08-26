using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SaveSystem
{
    public static void SavePlayer(PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        playerData.money = PlayerStats.Money;
        playerData.lives = PlayerStats.Lives;
        playerData.rounds = PlayerStats.Rounds;
        playerData.currentLevel = SceneManager.GetActiveScene().name;

        // Save nodes
        Node[] nodes = GameObject.FindObjectsOfType<Node>();
        foreach (Node node in nodes)
        {
            if (node.turret != null)
            {
                NodeData nodeData = new NodeData
                {
                    position = new SerializableVector3(node.transform.position),
                    turretName = node.turret.name,
                    isUpgraded = node.isUpgraded,
                    isFinalUpgraded = node.isFinalUpgraded
                };
                playerData.nodes.Add(nodeData);
            }
        }

        // Save enemies
        Enemies[] enemies = GameObject.FindObjectsOfType<Enemies>();
        foreach (Enemies enemy in enemies)
        {
            EnemyData enemyData = new EnemyData
            {
                position = new SerializableVector3(enemy.transform.position),
                enemyName = enemy.gameObject.tag,
                health = enemy.GetHealth(),
                waypointIndex = enemy.GetCurrentWaypointIndex()
            };
            playerData.enemies.Add(enemyData);
        }

        formatter.Serialize(stream, playerData);
        stream.Close();
    }



    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

           
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
