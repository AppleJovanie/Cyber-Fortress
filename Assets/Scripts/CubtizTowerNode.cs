using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubtizTowerNode : MonoBehaviour
{
    private BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        buildManager.SelectCubitzUI(this);
    }


}
