using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float Speed = 10f;

    private Transform target;
    private int wavePointIndex = 0;

     void Start()
    {
        target = WayPointSystem.wayPoints[0];
    }

     void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }
    void GetNextWayPoint()
    {
        if (wavePointIndex >= WayPointSystem.wayPoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavePointIndex++;
        target = WayPointSystem.wayPoints[wavePointIndex];
    }
}
