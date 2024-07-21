using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check the tag of the colliding object and reduce PlayerStats.Lives accordingly
        switch (other.tag)
        {
            case "TrojanHorse":
                PlayerStats.Lives -= 10;
                Debug.Log("Lives Deducted" +  PlayerStats.Lives);
                break;
            case "ComputerWorm":
                PlayerStats.Lives -= 15;
                break;
            case "Spyware":
                PlayerStats.Lives -= 18;
                break;
            case "Adware":
                PlayerStats.Lives -= 20;
                break;
            case "Malware":
                PlayerStats.Lives -= 50;
                break;
            default:
                Debug.LogWarning("Unknown enemy type: " + other.tag);
                break;
        }

        // Decrease the EnemiesAlive count and destroy the enemy
        WaveSpawner.EnemiesAlive--;
        Destroy(other.gameObject);
    }
}
