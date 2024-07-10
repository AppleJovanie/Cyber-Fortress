using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemies : MonoBehaviour
{
   
    public float speed = 10f;
    public int startHealth = 100;
    private float health;
    public int value = 50;
    private Transform target;
    private int wavePointIndex = 0;

    [Header("Unity Stuff")]
    public Image healthBar;
    void Start()
    {
     
        health = startHealth;
        target = WayPointSystem.wayPoints[0];
    }

    public void TakeDamage(int amount)
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
        Destroy(gameObject);
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPointSystem.wayPoints.Length - 1)
        {
            EndPath();
            return;
        }
        wavePointIndex++;
        target = WayPointSystem.wayPoints[wavePointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
