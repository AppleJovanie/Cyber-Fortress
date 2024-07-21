using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints2 : MonoBehaviour
{
    public static Transform[] wayPoints2;

    void Awake()
    {
        wayPoints2 = new Transform[transform.childCount];
        for (int i = 0; i < wayPoints2.Length; i++)
        {
            wayPoints2[i] = transform.GetChild(i);
        }
    }
}
