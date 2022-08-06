using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class Tower : MonoBehaviour
{
    public Vector2Int Coordinates { get { return coordinates; } }
    public int Value { get { return value; } }

    [SerializeField] private int value = 50;
    [SerializeField] private int powerLevel = 0;
    
    GridManager gridManager;
    bool isMerging = false;
    Vector2Int coordinates;
    TowerMenuController towerMenu;
    Pathfinder pathfinder;
    PrefabManager prefabManager;
    Light powerLevelLight;
    AlertController alert;

    void Awake()
    {
        // Instantiate starting tower weapon
        BuildTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon1);

        gridManager = FindObjectOfType<GridManager>();
        towerMenu = FindObjectOfType<TowerMenuController>();
        pathfinder = FindObjectOfType<Pathfinder>();
        prefabManager = FindObjectOfType<PrefabManager>();
        alert = FindObjectOfType<AlertController>();
        
        SetLightByPowerLevel();
    }

    void Update()
    {
        if (isMerging && Input.GetMouseButtonDown(0))
        {
            GetTowerClicked();
        }
        else if (isMerging && Input.GetMouseButtonUp(1))
        {
            Debug.Log("Merging cancelled");
            isMerging = false;
        }
    }


    void OnMouseOver()
    {
        if (gridManager.GetNode(coordinates).placedTower != null) // Tower placed in current tile
        {
            if (Input.GetMouseButtonDown(0))
            {
                BindAndDisplaySelectedTowerMenu();
            }
        }
        else if (Input.GetMouseButtonDown(0) && towerMenu.IsOpened) // Tower menu is open and we click on anywhere but another tower
        {
            towerMenu.ToggleVisibility(false);
            gridManager.SelectNodeTile(new Vector2Int(-1, -1)); // To deselect towers w/o selecting
        }
    }

    void IncrementPowerLevel()
    {
        this.powerLevel++;
    }

    void SetLightByPowerLevel()
    {
        this.powerLevelLight = GetComponentInChildren<Light>();

        switch (powerLevel)
        {
            case 0:
            {
                powerLevelLight.intensity = 1f;
                break;
            }
            case 3:
            {
                powerLevelLight.intensity = 15f;
                break;
            }
            case 5:
            {
                powerLevelLight.intensity = 50f;
                powerLevelLight.color = Color.red;
                break;
            }
        }
    }

    void BindAndDisplaySelectedTowerMenu()
    {
        towerMenu.ToggleVisibility(true);
        towerMenu.BindSelectedTower(coordinates);
        gridManager.SelectNodeTile(coordinates);
    }


    void GetTowerClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        
        if (Physics.Raycast(ray, out rayHit, 200.0f))
        {
            if (rayHit.collider)
            {
                if (rayHit.collider.gameObject.GetComponent<Tower>() != null) // Tower clicked
                {
                    MergeTowers(rayHit.collider.gameObject.GetComponent<Tower>().Coordinates);
                }
            }
        }
    }

    void MergeTowers(Vector2Int destTowerCoords)
    {
        Tower destTower = gridManager.Grid[destTowerCoords].placedTower;

        if (this.powerLevel > destTower.powerLevel)
        {
            alert.Alert("Cannot merge a stronger tower to a weaker one");
            isMerging = false;
            return;
        }

        // Transfer stats from dest tower before destroying
        destTower.GetComponentInChildren<Weapon>().MergeWeaponStats(GetComponentInChildren<Weapon>());
        destTower.IncrementPowerLevel();
        destTower.SetLightByPowerLevel();

        // Destroy previous towers
        gridManager.DestroyTowerAndRemoveFromNode(coordinates);
        

        isMerging = false;
    }

    public Tower CreateTower(Vector3 position)
    {
        var bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return null;
        }

        return bank.WithdrawBalance(value) ? BuildTower(position) : null;
    }


    public Tower BuildTower(Vector3 position)
    {
        var prefabs = FindObjectOfType<PrefabManager>();
        // Instantiate tower base
        var tower = Instantiate(prefabs.GetPrefab(PrefabManager.PrefabIndices.Tower),
                position,
                Quaternion.identity)
            .GetComponent<Tower>();

        if (!gridManager)
        {
            gridManager = FindObjectOfType<GridManager>();
        }

        tower.coordinates = gridManager.GetCoordinatesFromPosition(position);
        gridManager.BlockNode(tower.coordinates);
        gridManager.SetNodeTower(tower, tower.coordinates);


        if (!pathfinder)
        {
            pathfinder = FindObjectOfType<Pathfinder>();
        }
        pathfinder.BroadcastRecalculatePath();

        return tower;
    }

    public void BuildTowerWeapon(PrefabManager.PrefabIndices weaponIndex)
    {
        var prefabs = FindObjectOfType<PrefabManager>();

        var weapon = GetComponentInChildren<Weapon>();
        if (weapon != null)
        {
            Destroy(weapon.gameObject);
        }

        Instantiate(prefabs.GetPrefab(weaponIndex),
            gameObject.transform.position + prefabs.GetPrefabPosition(weaponIndex),
            Quaternion.identity, gameObject.transform).GetComponent<Weapon>();
    }

    public void BeginMerge()
    {
        isMerging = true;
        Debug.Log("Now merging");
    }
}