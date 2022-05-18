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
    Tower placedTower;
    TileState state = TileState.Vacant;
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
                state = TileState.Blocked;
            }
        }
    }

    void OnMouseOver()
    {
        if (state == TileState.Vacant && !towerMenu.IsOpened)
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
        else if (state == TileState.TowerPlaced)
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
                gridManager.SelectTile(new Vector2Int(-1, -1)); // To deselect towers w/o selecting
            }
        }
    }

    void BindAndDisplaySelectedTowerMenu()
    {
        towerMenu.ToggleVisibility(true);
        towerMenu.BindSelectedTower(placedTower);
        gridManager.SelectTile(coordinates);
    }

    void InstantiateTower()
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            placedTower = prefabManager.GetPrefab(PrefabManager.PrefabIndices.Tower).GetComponent<Tower>()
                .CreateTower(transform.position);
            if (placedTower != null)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.BroadcastRecalculatePath();
                state = TileState.TowerPlaced;
            }
        }
    }

    void InstantiateObstacle()
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            bool success = prefabManager.GetPrefab(PrefabManager.PrefabIndices.Obstacle).GetComponent<Obstacle>()
                .CreateObstacle(transform.position);
            if (success)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.BroadcastRecalculatePath();
                state = TileState.ObstaclePlaced;
            }
        }
    }
}