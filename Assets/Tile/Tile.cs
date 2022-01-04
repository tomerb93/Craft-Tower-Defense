using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] bool isPlaceable;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Tower towerPrefab;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates;
    TowerMenuController towerMenu;
    Tower placedTower;
    TileState state = TileState.VACANT;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
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
                state = TileState.BLOCKED;
            }
        }
    }

    void OnMouseOver()
    {
        if (state == TileState.VACANT && !towerMenu.IsOpened)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InstantiateTower(towerPrefab);
            }

            if (Input.GetMouseButtonDown(1))
            {
                InstantiateObstacle(obstaclePrefab);
            }
        }
        else if (state == TileState.TOWER_PLACED)
        {
            // Display tower menu and bind currently selected tower to UI
            if (Input.GetMouseButtonDown(0))
            {
                towerMenu.ToggleVisibility(true);
                towerMenu.BindSelectedTower(placedTower);
            }
        }
    }

    void InstantiateTower(Tower towerPrefab)
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            placedTower = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (placedTower != null)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.BroadcastRecalculatePath();
                state = TileState.TOWER_PLACED;
            }
        }
    }

    void InstantiateObstacle(GameObject obstaclePrefab)
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
            gridManager.BlockNode(coordinates);
            pathfinder.BroadcastRecalculatePath();
            state = TileState.OBSTACLE_PLACED;
        }
    }

    enum TileState
    {
        VACANT,
        BLOCKED,
        TOWER_PLACED,
        OBSTACLE_PLACED
    }
}
