using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        int damage = 0; // Initialize damage variable
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
        if (damage > 0)
        {
            PlayerStats.Lives = Mathf.Max(0, PlayerStats.Lives - damage); // Prevent negative lives
            Debug.Log("Lives Deducted: " + PlayerStats.Lives);

            if (PlayerStats.Lives <= 0)
            {
                GameOver();
            }
        }

        // Decrease the EnemiesAlive count and destroy the enemy
        WaveSpawner.EnemiesAlive--;
        Destroy(other.gameObject);
    }
    void GameOver()
    {
        FreezeGame(true);

    }
    private void FreezeGame(bool freeze)
    {
        Time.timeScale = freeze ? 0f : 1f; // Freeze or unfreeze the game
    }
}
