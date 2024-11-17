using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndPoint : MonoBehaviour
{
    public Text LivesText;

    private void OnTriggerEnter(Collider other)
    {
        int damage = 0; // Initialize damage variable

        // Determine the damage based on the tag of the colliding object
        switch (other.tag)
        {
            case "TrojanHorse":
                damage = 10;
                break;
            case "ComputerWorm":
                damage = 15;
                break;
            case "Spyware":
                damage = 18;
                break;
            case "Adware":
                damage = 20;
                break;
            case "Malware":
                damage = 50;
                break;
            default:
                Debug.LogWarning("Unknown enemy type: " + other.tag);
                break;
        }

        // Apply the damage and prevent lives from going negative
        if (damage > 0)
        {
            PlayerStats.Lives = Mathf.Max(0, PlayerStats.Lives - damage);

            // Update the LivesText to show the damage taken
            LivesText.text = $"-{damage} Damage! Lives Left: {PlayerStats.Lives}";

            Debug.Log($"Damage Taken: -{damage}, Lives Left: {PlayerStats.Lives}");

            // Check if the player has run out of lives
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
