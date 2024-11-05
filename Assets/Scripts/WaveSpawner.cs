using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public Wave[] waves2;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public float timeBetweenWaves = 5f;
    private float countDown;
    private int waveIndex1 = 0;
    private int waveIndex2 = 0;
    public Vector3 enemyRotation = new Vector3(0, 90.9f, 0);
    public GameObject ProceedToNextLevelPanel;

    public static int EnemiesAlive = 0;

    public TextMeshProUGUI waveCountDownText;
    public TextMeshProUGUI waveMessageText;

    void Start()
    {
        Time.timeScale = 1f;
        countDown = 1.5f;
        EnemiesAlive = 0;

        int totalEnemies = CalculateTotalEnemies();
        KillTracker.instance.SetTotalEnemies(totalEnemies);

        Debug.Log("Total enemies to kill: " + totalEnemies);
    }

    void Update()
    {
        // Check if no enemies are left and all waves are completed
        if (EnemiesAlive <= 0 && waveIndex1 >= waves.Length && (waves2 == null || waveIndex2 >= waves2.Length))
        {
            // Extra check to confirm no remaining enemy objects in the scene
            if (GameObject.FindWithTag("Enemy") == null)
            {
                Debug.Log("All waves completed and all enemies cleared. Showing 'You Won' panel.");
                ProceedToNextLevelPanel.SetActive(true);
                enabled = false;
                return;
            }
        }

        // Handle countdown to next wave
        if (countDown <= 0f && EnemiesAlive == 0)
        {
            if (waveIndex1 < waves.Length || (waves2 != null && waveIndex2 < waves2.Length))
            {
                countDown = timeBetweenWaves;
                StartCoroutine(FreezeTimerAtZero());
            }
        }

        waveCountDownText.text = string.Format("{0:00.00}", Mathf.Max(0f, countDown));
        countDown -= Time.deltaTime;
    }



    int CalculateTotalEnemies()
    {
        int totalEnemies = 0;
        foreach (Wave wave in waves) totalEnemies += wave.count;
        if (waves2 != null)
            foreach (Wave wave in waves2) totalEnemies += wave.count;

        return totalEnemies;
    }

    IEnumerator FreezeTimerAtZero()
    {
        waveCountDownText.text = "00.00";
        yield return new WaitForSeconds(4f);

        if (waveIndex1 < waves.Length || (waves2 != null && waveIndex2 < waves2.Length))
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
            waveMessageText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration));
            yield return null;
        }

        waveMessageText.gameObject.SetActive(false);
        waveMessageText.color = originalColor;

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        PlayerStats.Rounds++;

        if (waveIndex1 < waves.Length)
        {
            Wave wave1 = waves[waveIndex1];
            StartCoroutine(SpawnWave(wave1, spawnPoint1, WayPointSystem.wayPoints, waveIndex1));
            waveIndex1++;
        }

        if (waves2 != null && waveIndex2 < waves2.Length)
        {
            Wave wave2 = waves2[waveIndex2];
            StartCoroutine(SpawnWave(wave2, spawnPoint2, Waypoints2.wayPoints2, waveIndex2));
            waveIndex2++;
        }

        yield return null;
    }

    IEnumerator SpawnWave(Wave wave, Transform spawnPoint, Transform[] waypoints, int waveNumber)
    {
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemyPrefab, spawnPoint, waypoints, waveNumber);
            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    void SpawnEnemy(GameObject enemy, Transform spawnPoint, Transform[] waypoints, int waveNumber)
    {
        GameObject enemyInstance = Instantiate(enemy, spawnPoint.position, Quaternion.Euler(enemyRotation));
        Enemies enemyScript = enemyInstance.GetComponent<Enemies>();

        if (enemyScript != null)
        {
            enemyScript.SetWaypoints(waypoints);
            enemyScript.IncreaseHealth(waveNumber);
        }

        EnemiesAlive++;
        Debug.Log("Enemy spawned. Current EnemiesAlive: " + EnemiesAlive);
    }

    public static void OnEnemyKilled()
    {
        EnemiesAlive = Mathf.Max(EnemiesAlive - 1, 0);
        Debug.Log("Enemy killed. Remaining EnemiesAlive: " + EnemiesAlive);
    }
}
