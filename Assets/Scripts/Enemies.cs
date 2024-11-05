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
        if (waypoints != null && waypoints.Length > 0)
        {
            target = waypoints[wavePointIndex]; // Ensure that the enemy starts at the correct waypoint
        }
        else
        {
            Debug.LogWarning("No waypoints assigned to enemy at start!");
        }
    }
    public void IncreaseHealth(int waveNumber)
    {
        // Scale health by multiplying the wave number by a factor
        float healthMultiplier = 1 + (waveNumber * 0.1f); // Adjust the multiplier as needed
        health = startHealth * healthMultiplier;
        healthBar.fillAmount = health / startHealth;
    }

    // Set the waypoints for the enemy to follow
    public void SetWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
        if (waypoints.Length > 0)
        {
            target = waypoints[wavePointIndex]; // Assign the initial target when waypoints are set
        }
    }

    // Set the current waypoint index and target it
    public void SetCurrentWaypointIndex(int index)
    {
        wavePointIndex = index;
        target = waypoints[wavePointIndex];
    }

    // Get the current waypoint index for saving
    public int GetCurrentWaypointIndex()
    {
        return wavePointIndex;
    }

    // Set the health and update the health bar
    public void SetHealth(float newHealth)
    {
        health = newHealth;
        healthBar.fillAmount = health / startHealth;
    }

    // Get the current health for saving
    public float GetHealth()
    {
        return health;
    }

    // Method to handle damage and death
    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    // Method to handle enemy death
    void Die()
    {
        PlayerStats.Money += value;
        KillTracker.instance.kills++;
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        WaveSpawner.EnemiesAlive--;
        Debug.Log("Enemy died. EnemiesAlive: " + WaveSpawner.EnemiesAlive); // Debug log
        Destroy(gameObject);
    }

    // Update is called once per frame
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

        // Move to the next waypoint when close to the current one
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    // Get the next waypoint and set it as the target
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

    // Handle what happens when the enemy reaches the end of the path
    void EndPath()
    {
        string enemyTag = gameObject.tag;

        switch (enemyTag)
        {
            case "TrojanHorse":
                PlayerStats.Lives -= 10;
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

        WaveSpawner.EnemiesAlive--;
        Debug.Log("Enemy reached end path. EnemiesAlive: " + WaveSpawner.EnemiesAlive); // Debug log
        Destroy(gameObject);
    }

    // Method to get data for saving the enemy
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

    // Method to set data when loading the enemy
    public void SetEnemyData(EnemyData data)
    {
        transform.position = data.position.ToVector3();
        health = data.health;
        healthBar.fillAmount = health / startHealth;
        wavePointIndex = data.waypointIndex;

        if (waypoints != null && waypoints.Length > 0)
        {
            target = waypoints[wavePointIndex]; // Ensure the enemy targets the correct waypoint after loading
        }
    }
}
