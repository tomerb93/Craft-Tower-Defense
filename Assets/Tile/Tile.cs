using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsPlaceable { get { return isPlaceable; } }

    [SerializeField] bool isPlaceable;
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject towerPrefab;

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
        if (Input.GetMouseButtonDown(0))
        {
            InstantiatePrefabOnTile(towerPrefab);
        }

        if (Input.GetMouseButtonDown(1))
        {
            InstantiatePrefabOnTile(obstaclePrefab);
        }
    }

    private void InstantiatePrefabOnTile(GameObject prefab)
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            gridManager.BlockNode(coordinates);
            pathfinder.BroadcastRecalculatePath();
        }
    }
}
