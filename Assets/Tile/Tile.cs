using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsPlaceable { get { return isPlaceable; } }
    
    [SerializeField] bool isPlaceable;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        
            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable &&
            !pathfinder.WillBlockPath(coordinates))
        {
            gridManager.BlockNode(coordinates);
            pathfinder.BroadcastRecalculatePath();
        }
    }
}
