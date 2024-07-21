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
    private float countDown = 2f;
    private int waveIndex1 = 0;
    private int waveIndex2 = 0;
    public Vector3 enemyRotation = new Vector3(0, 90.9f, 0); // Desired rotation for the enemy

    public static int EnemiesAlive = 0;

    public TextMeshProUGUI waveCountDownText;

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (countDown <= 0f)
        {
            if (waveIndex1 < waves.Length || waveIndex2 < waves2.Length)
            {
                StartCoroutine(SpawnWaves());
                countDown = timeBetweenWaves;
            }
            else
            {
                Debug.Log("All waves completed!");
                enabled = false; // Disable the WaveSpawner after all waves are completed
            }
        }

        countDown -= Time.deltaTime;

        waveCountDownText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator SpawnWaves()
    {
        PlayerStats.Rounds++;

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

        yield return null;
    }

    IEnumerator SpawnWave(Wave wave, Transform spawnPoint, Transform[] waypoints)
    {
        float spawnInterval = wave.spawnInterval;

        for (int i = 0; i < wave.count; i++)
        {
            Debug.Log("Get ready for another wave");
            SpawnEnemy(wave.enemyPrefab, spawnPoint, waypoints);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(GameObject enemy, Transform spawnPoint, Transform[] waypoints)
    {
        Vector3 spawnPosition = spawnPoint.position;
        Quaternion rotation = Quaternion.Euler(enemyRotation);
        Debug.Log($"Spawning enemy at: {spawnPosition} with offset: ");

        GameObject enemyInstance = Instantiate(enemy, spawnPosition, rotation);

        // Attach a script to make the enemy follow waypoints
        Enemies enemyScript = enemyInstance.GetComponent<Enemies>();
        if (enemyScript != null)
        {
            enemyScript.SetWaypoints(waypoints);
        }

        EnemiesAlive++;
    }
}
