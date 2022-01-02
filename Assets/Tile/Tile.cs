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
    TowerMenuController towerMenu;

    GameObject placedObject;

    State currentState = State.VACANT;

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
                currentState = State.BLOCKED;
            }
        }
    }

    void OnMouseOver()
    {
        if (currentState == State.VACANT && !towerMenu.IsOpened)
        {
            if (Input.GetMouseButtonDown(0))
            {
                InstantiatePrefabOnTile(towerPrefab, State.TOWER_PLACED);
            }

            if (Input.GetMouseButtonDown(1))
            {
                InstantiatePrefabOnTile(obstaclePrefab, State.OBSTACLE_PLACED);
            }
        }
        else if (currentState == State.TOWER_PLACED)
        {
            // Display tower menu and bind currently selected tower to UI
            if (Input.GetMouseButtonDown(0))
            {
                towerMenu.ToggleVisibility(true);
                towerMenu.BindSelectedTower(placedObject);
            }
        }
    }

    private void InstantiatePrefabOnTile(GameObject prefab, State newState)
    {
        if (!pathfinder.WillBlockPath(coordinates))
        {
            placedObject = Instantiate(prefab, transform.position, Quaternion.identity);
            gridManager.BlockNode(coordinates);
            pathfinder.BroadcastRecalculatePath();
            currentState = newState;
        }
    }

    enum State
    {
        VACANT,
        BLOCKED,
        TOWER_PLACED,
        OBSTACLE_PLACED
    }
}
