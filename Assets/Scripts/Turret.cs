using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemies targetEnemy;
    private Transform previousTarget;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (Default)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountDown = 0f;

    [Header("Unity Setup Fields")]
    public List<string> enemyTags = new List<string> { "TrojanHorse", "ComputerWorm", "Spyware", "Adware", "Malware" };
    public Transform partToRotate;
    public float turnSpeed = 10f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public float damageOverTime = 30f; // Damage per second
    public float slowAmount = 0.5f; // Slow percentage

    public Transform firePoint;

    AudioManager audioManager;

    public void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        List<GameObject> allEnemies = new List<GameObject>();

        foreach (string tag in enemyTags)
        {
            GameObject[] enemiesWithTag = GameObject.FindGameObjectsWithTag(tag);
            allEnemies.AddRange(enemiesWithTag);
        }

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in allEnemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && IsTargetable(enemy.tag))
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemies>();
        }
        else
        {
            target = null;
            targetEnemy = null;
        }
    }

    bool IsTargetable(string tag)
    {
        return enemyTags.Contains(tag);
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            return;
        }

        // Target Lock On
        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = 1f / fireRate;
            }

            fireCountDown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        if (target == null || Vector3.Distance(transform.position, target.position) > range)
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                if (AudioManager.Instance.laser.isPlaying)
                {
                    AudioManager.Instance.laser.Stop();
                }
            }
            return;
        }

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        // Check if the target has changed, and if so, restart the audio
        if (target != previousTarget)
        {
            if (AudioManager.Instance.laser.isPlaying)
            {
                AudioManager.Instance.laser.Stop();
            }
            AudioManager.Instance.laser.Play();
            previousTarget = target; // Update previousTarget to the current one
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        // Damage the enemy
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        }
    }

    void Shoot()
    {
        GameObject bulletGo = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
