using UnityEngine;
using UnityEngine.UI;

public class Enemies : MonoBehaviour
{
    public float speed = 10f;
    public int startHealth = 100;
    public float health;
    public int value = 50;
    private Transform target;
    private int wavePointIndex = 0;

    public GameObject head; // Reference to the head GameObject
    public GameObject deathEffect;
    public Image healthBar;

    private Transform[] waypoints;

    void Start()
    {
        health = startHealth;
        target = waypoints[0];
    }

    public void SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }

    public void SetCurrentWaypointIndex(int index)
    {
        wavePointIndex = index;
        target = waypoints[wavePointIndex];
    }

    public int GetCurrentWaypointIndex()
    {
        return wavePointIndex;
    }

    public void SetHealth(float newHealth)
    {
        health = newHealth;
        healthBar.fillAmount = health / startHealth;
    }

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        PlayerStats.Money += value;
        KillTracker.instance.kills++;
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned to enemy!");
            return;
        }

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Update head rotation to face the direction of movement
        if (head != null)
        {
            Vector3 headDirection = (target.position - head.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(headDirection);
            head.transform.rotation = Quaternion.Lerp(head.transform.rotation, lookRotation, Time.deltaTime * speed);
        }

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= waypoints.Length - 1)
        {
            EndPath();
            return;
        }
        wavePointIndex++;
        target = waypoints[wavePointIndex];
    }

    void EndPath()
    {
        // Determine which enemy type this is based on its tag
        string enemyTag = gameObject.tag; // Assuming each enemy has its tag set correctly

        // Reduce player lives based on the enemy tag
        switch (enemyTag)
        {
            case "TrojanHorse":
                PlayerStats.Lives -= 10;
                Debug.Log("Lives Deducted: " + PlayerStats.Lives);
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
                Debug.LogWarning("Unknown enemy type: " + enemyTag);
                break;
        }

        // Decrease the EnemiesAlive count and destroy the enemy
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    // Method to get data for saving
    public EnemyData GetEnemyData()
    {
        return new EnemyData
        {
            position = new SerializableVector3(transform.position),
            enemyName = gameObject.tag,
            health = health,
            waypointIndex = wavePointIndex
        };
    }

    // Method to set data when loading
    public void SetEnemyData(EnemyData data)
    {
        transform.position = data.position.ToVector3();
        health = data.health;
        healthBar.fillAmount = health / startHealth;
        wavePointIndex = data.waypointIndex;

        // If waypoints are set, adjust the target accordingly
        if (waypoints != null && waypoints.Length > 0)
        {
            target = waypoints[wavePointIndex];
        }
    }
}
