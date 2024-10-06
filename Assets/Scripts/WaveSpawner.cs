using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Wave[] waves2; // Second set of waves
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public float timeBetweenWaves = 5f;
    private float countDown;
    private int waveIndex1 = 0;
    private int waveIndex2 = 0;
    public Vector3 enemyRotation = new Vector3(0, 90.9f, 0); // Desired rotation for the enemy
    public GameObject ProceedToNextLevelPanel;

    public static int EnemiesAlive = 0;  // Tracks enemies still alive in the scene

    public TextMeshProUGUI waveCountDownText;
    public TextMeshProUGUI waveMessageText; // Reference to the TextMeshProUGUI for wave message

    void Start()
    {
        Time.timeScale = 1f; // Ensure the game is running
        countDown = 1.5f; // Initialize countdown to 1.5 seconds for player preparation
        EnemiesAlive = 0; // Reset enemies alive count

        // Calculate total enemies across both sets of waves
        int totalEnemies = 0;
        foreach (Wave wave in waves)
        {
            totalEnemies += wave.count;
        }
        foreach (Wave wave in waves2)
        {
            totalEnemies += wave.count;
        }

        // Set the totalEnemies in the KillTracker
        KillTracker.instance.SetTotalEnemies(totalEnemies);

        Debug.Log("Total enemies to kill: " + totalEnemies);
    }

    void Update()
    {
        // Check if any enemies are alive
       

        // Don't proceed if there are enemies still alive
        if (EnemiesAlive > 0)
        {
            return;
        }

        // Check if the player has killed all enemies
        if (KillTracker.instance.HasKilledAllEnemies())
        {
            Debug.Log("Player has killed all enemies! Showing 'You Won' panel.");
            ProceedToNextLevelPanel.SetActive(true); // Show the panel
            enabled = false; // Disable the WaveSpawner after all waves and enemies are done
            return;
        }

        // Freeze the countdown timer when wave starts and wait for 4 seconds before spawning
        if (countDown <= 0f)
        {
            if (waveIndex1 >= waves.Length && waveIndex2 >= waves2.Length && EnemiesAlive == 0)
            {
                Debug.Log("All waves completed and all enemies destroyed!");

                // Enable the ProceedToNextLevelPanel
                ProceedToNextLevelPanel.SetActive(true);

                enabled = false; // Disable the WaveSpawner after all waves and enemies are done
            }
            else
            {
                countDown = 0f;
                StartCoroutine(FreezeTimerAtZero()); // Freeze timer at zero before wave starts
                countDown = timeBetweenWaves; // Reset the countdown for the next wave
            }
        }

        // Update the countdown text, freeze at zero during the wave
        waveCountDownText.text = string.Format("{0:00.00}", Mathf.Max(0f, countDown));

        countDown -= Time.deltaTime;
    }



    IEnumerator FreezeTimerAtZero()
    {
        // Display zero and freeze the timer for 4 seconds to allow player preparation
        waveCountDownText.text = "00.00";

        // Wait for 4 seconds to give the player time to prepare
        yield return new WaitForSeconds(4f);

        // After the 4-second freeze, show the wave message and start spawning the wave
        StartCoroutine(DisplayWaveMessage());
    }

    IEnumerator DisplayWaveMessage()
    {
        int currentWave = Mathf.Max(waveIndex1, waveIndex2) + 1;
        waveMessageText.text = "Wave " + currentWave + " Good Luck";
        waveMessageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        float fadeDuration = 1.5f;
        float elapsedTime = 0f;
        Color originalColor = waveMessageText.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            waveMessageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        waveMessageText.gameObject.SetActive(false);
        waveMessageText.color = originalColor; // Reset the color for next use

        StartCoroutine(SpawnWaves()); // Start spawning the waves after the message
    }

    IEnumerator SpawnWaves()
    {
        PlayerStats.Rounds++; // Increment the round count

        if (waveIndex1 < waves.Length)
        {
            Wave wave1 = waves[waveIndex1];
            StartCoroutine(SpawnWave(wave1, spawnPoint1, WayPointSystem.wayPoints)); // Use WayPointSystem.wayPoints for spawnPoint1
            waveIndex1++;
        }

        if (waveIndex2 < waves2.Length)
        {
            Wave wave2 = waves2[waveIndex2];
            if (wave2 != null)
            {
                StartCoroutine(SpawnWave(wave2, spawnPoint2, Waypoints2.wayPoints2)); // Use Waypoints2.wayPoints2 for spawnPoint2
            }
            waveIndex2++;
        }

        yield return null; // Ensures that the method returns an IEnumerator
    }


    IEnumerator SpawnWave(Wave wave, Transform spawnPoint, Transform[] waypoints)
    {
        float spawnInterval = wave.spawnInterval;

        for (int i = 0; i < wave.count; i++)
        {
            Debug.Log("Spawning enemy");
            SpawnEnemy(wave.enemyPrefab, spawnPoint, waypoints);
            yield return new WaitForSeconds(spawnInterval); // Wait between enemy spawns
        }
    }

    void SpawnEnemy(GameObject enemy, Transform spawnPoint, Transform[] waypoints)
    {
        Vector3 spawnPosition = spawnPoint.position;
        Quaternion rotation = Quaternion.Euler(enemyRotation);
        Debug.Log($"Spawning enemy at: {spawnPosition}");

        GameObject enemyInstance = Instantiate(enemy, spawnPosition, rotation);

        // Attach a script to make the enemy follow waypoints
        Enemies enemyScript = enemyInstance.GetComponent<Enemies>();
        if (enemyScript != null)
        {
            enemyScript.SetWaypoints(waypoints);
        }

        EnemiesAlive++;
    }

    // Call this method when an enemy dies
    public static void OnEnemyKilled()
    {
        EnemiesAlive--;
    }
}
