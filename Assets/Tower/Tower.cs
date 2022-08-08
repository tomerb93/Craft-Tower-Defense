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

    public int MergeCost
    {
        get { return (int)Mathf.Pow(2, powerLevel) * value; }
    }

    public int PowerLevel
    {
        get { return powerLevel; }
    }

    [SerializeField] private int value = 50;
    [SerializeField] private int powerLevel = 0;
    
    GridManager gridManager;
    Vector2Int coordinates;
    TowerMenuController towerMenu;
    Pathfinder pathfinder;
    Light powerLevelLight;
    AlertController alert;
    bool isMerging;

    void Awake()
    {
        // Instantiate starting tower weapon
        BuildTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon1);

        gridManager = FindObjectOfType<GridManager>();
        towerMenu = FindObjectOfType<TowerMenuController>();
        pathfinder = FindObjectOfType<Pathfinder>();
        alert = FindObjectOfType<AlertController>();
        
        SetLightByPowerLevel();
    }

    void Update()
    {
        if (isMerging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetTowerClicked();
            }
        }
    }


    void OnMouseOver()
    {
        if (gridManager.GetNode(coordinates).placedTower != null && !isMerging) // Tower placed in current tile
        {
            if (Input.GetMouseButtonDown(0))
            {
                BindAndDisplaySelectedTower();
            }
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
                powerLevelLight.intensity = 0f;
                break;
            case 1:
            {
                powerLevelLight.intensity = 1f;
                break;
            }
            case 2:
            {
                powerLevelLight.intensity = 5f;
                break;
            }
            case 5:
            {
                powerLevelLight.intensity = 10f;
                break;
            }
            case 10:
            {
                powerLevelLight.intensity = 15f;
                powerLevelLight.color = Color.red;
                break;
            }
        }
    }

    void BindAndDisplaySelectedTower()
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
                else
                {
                    alert.Alert("Fusing cancelled");
                    isMerging = false;
                }
            }
        }
    }

    void MergeTowers(Vector2Int destTowerCoords)
    {
        var bank = FindObjectOfType<Bank>();
        Tower destTower = gridManager.Grid[destTowerCoords].placedTower;

        if (!bank.WithdrawBalance(MergeCost))
        {
            alert.Alert("Not enough funds to fuse");
            isMerging = false;
            return;
        }

        if (GetTowerWeaponType() != destTower.GetTowerWeaponType())
        {
            alert.Alert("Cannot merge two different types of towers");
            isMerging = false;
            return;
        }

        if (destTowerCoords == coordinates)
        {
            alert.Alert("Cannot fuse into the same tower");
            isMerging = false;
            return;
        }
        

        // Transfer stats from src tower before destroying
        destTower.GetComponentInChildren<Weapon>().MergeWeaponStats(GetComponentInChildren<Weapon>());
        destTower.powerLevel += (powerLevel + 1);
        destTower.SetLightByPowerLevel();

        // Destroy src tower
        gridManager.DestroyTowerAndRemoveFromNode(coordinates);
        destTower.BindAndDisplaySelectedTower();
        gridManager.SelectNodeTile(destTowerCoords);

        isMerging = false;
    }

    public Tower CreateTower(Vector3 position)
    {
        var bank = FindObjectOfType<Bank>();
        towerMenu = FindObjectOfType<TowerMenuController>();

        if (bank == null)
        {
            return null;
        }

        return bank.WithdrawBalance(value) ? BuildTower(position) : null;
    }

    public PrefabManager.PrefabIndices GetTowerWeaponType()
    {
        return GetComponentInChildren<Weapon>().WeaponType;
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

        weapon = Instantiate(prefabs.GetPrefab(weaponIndex),
            gameObject.transform.position + prefabs.GetPrefabPosition(weaponIndex),
            Quaternion.identity, gameObject.transform).GetComponent<Weapon>();

        if (weapon != null)
        {
            weapon.WeaponType = weaponIndex;
        }
    }

    public void BeginMerge()
    {
        isMerging = true;
    }
}