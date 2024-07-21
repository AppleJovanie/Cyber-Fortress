using System;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBluePrint turretToBuild;
    private Node selectedNode;
    private CubtizTowerNode selectedTowerNode;

    public NodeUI nodeUI;
    public CubitzTowerUI towerUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    //Selecting CubitzTower
    public void SelectCubitzUI(CubtizTowerNode towerNode)
    {
        if (selectedTowerNode == towerNode)
        {
            DeselectCubitzUI();
            return;
        }

        selectedTowerNode = towerNode;
        turretToBuild = null;

        towerUI.SetTarget(towerNode);
    }

    public void DeselectCubitzUI()
    {
        selectedTowerNode = null;
        towerUI.Hide();
    }

    public void SelectTurretToBuild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
        DeselectCubitzUI();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }

    internal void SelectCubitzUI(CubitzTowerUI towerUI)
    {
        throw new NotImplementedException();
    }
}
