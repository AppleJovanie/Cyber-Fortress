using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform SpawnPoint;
    public float TimeBetweenWaves = 5f;
    private float CountDown = 2f;
    private int waveIndex = 0;
    public float spawnOffset = 1f;  // Distance between each enemy in a wave
    public Vector3 enemyRotation = new Vector3(0, 90.9f, 0); // Desired rotation for the enemy

    public TextMeshProUGUI waveCountDownText;

    void Update()
    {
        if (CountDown < 0f)
        {
            StartCoroutine(SpawnWave());
            CountDown = TimeBetweenWaves;
        }
        CountDown -= Time.deltaTime;

        waveCountDownText.text = Mathf.Round(CountDown).ToString();
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy(i);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy(int index)
    {
        Vector3 offset = new Vector3(index * spawnOffset, 0, 0);  // Adjust the offset as needed
        Vector3 spawnPosition = SpawnPoint.position + offset;
        Quaternion rotation = Quaternion.Euler(enemyRotation); // Create a quaternion from the desired rotation
        Instantiate(enemyPrefab, spawnPosition, rotation); // Use the rotation when instantiating the enemy
    }
}
