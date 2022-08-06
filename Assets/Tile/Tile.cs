using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates;

    PrefabManager prefabManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        prefabManager = FindObjectOfType<PrefabManager>();
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
            gridManager.GetNode(coordinates).isWalkable)
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