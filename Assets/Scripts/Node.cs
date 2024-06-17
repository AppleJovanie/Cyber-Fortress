using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset; // Use this to adjust the turret position for each turret type


    private GameObject turret;
    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't Build there");
            return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        if (turretToBuild == null)
        {
            Debug.LogError("No turret selected to build.");
            return;
        }

        // Adjust the position by adding the positionOffset to ensure it is placed correctly on top
        Vector3 spawnPosition = transform.position + positionOffset;
        turret = Instantiate(turretToBuild, spawnPosition, transform.rotation);
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
