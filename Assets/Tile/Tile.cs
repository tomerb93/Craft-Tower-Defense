using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileState
    {
        Vacant,
        Blocked,
        TowerPlaced,
        ObstaclePlaced
    }

    [SerializeField] bool isPlaceable;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates;
    TowerMenuController towerMenu;
    PrefabManager prefabManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        prefabManager = FindObjectOfType<PrefabManager>();
        towerMenu = FindObjectOfType<TowerMenuController>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseOver()
    {
        if (!gridManager.NodeIsTaken(coordinates) && 
            gridManager.GetNode(coordinates).isWalkable &&
            !towerMenu.IsOpened)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InstantiateTower();
            }

            if (Input.GetMouseButtonDown(1))
            {
                InstantiateObstacle();
            }
        }
        else if (gridManager.GetNode(coordinates).placedTower != null) // Tower placed in current tile
        {
            if (Input.GetMouseButtonDown(0))
            {
                BindAndDisplaySelectedTowerMenu();
            }
        }
        else
        {
            // Tower is selected and we click on anywhere but another tower
            if (Input.GetMouseButtonDown(0))
            {
                towerMenu.ToggleVisibility(false);
                gridManager.SelectNodeTile(new Vector2Int(-1, -1)); // To deselect towers w/o selecting
            }
        }
    }

    void BindAndDisplaySelectedTowerMenu()
    {
        towerMenu.ToggleVisibility(true);
        towerMenu.BindSelectedTower(coordinates);
        gridManager.SelectNodeTile(coordinates);
    }

    void InstantiateTower()
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            Tower placedTower = prefabManager.GetPrefab(PrefabManager.PrefabIndices.Tower).GetComponent<Tower>()
                .CreateTower(transform.position);
            
            if (!placedTower) return;

            gridManager.BlockNode(coordinates);
            gridManager.SetNodeTower(placedTower, coordinates);

            pathfinder.BroadcastRecalculatePath();
        }
    }

    void InstantiateObstacle()
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            bool success = prefabManager.GetPrefab(PrefabManager.PrefabIndices.Obstacle).GetComponent<Obstacle>()
                .CreateObstacle(transform.position);
            
            if (!success) return;

            gridManager.BlockNode(coordinates);
            gridManager.GetNode(coordinates).obstaclePlaced = true;

            pathfinder.BroadcastRecalculatePath();
        }
    }
}