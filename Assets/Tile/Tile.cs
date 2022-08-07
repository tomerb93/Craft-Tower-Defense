using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
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
        else if (Input.GetMouseButtonDown(0) && towerMenu.IsOpened) // Tower menu is open and we click on anywhere but another tower
        {
            towerMenu.ToggleVisibility(false);
            gridManager.SelectNodeTile(new Vector2Int(-1, -1)); // To deselect towers w/o selecting
        }
    }


    void InstantiateTower()
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            Tower placedTower = prefabManager.GetPrefab(PrefabManager.PrefabIndices.Tower).GetComponent<Tower>()
                .CreateTower(transform.position);
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