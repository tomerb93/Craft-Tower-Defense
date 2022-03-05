using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private enum LabelerState
    {
        Coordinates,
        O
    }

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    readonly Color defaultColor = Color.white;
    readonly Color blockedColor = Color.clear;
    readonly Color exploredColor = Color.yellow;
    readonly Color pathColor = Color.red;
    readonly Color selectedColor = Color.green;

    LabelerState state;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        state = LabelerState.O;
    }

    void Update()
    {
        ProcessLabels();
        UpdateTileName();
        SetLabelColor();
        ToggleLabelsDisplay();
    }

    void ToggleLabelsDisplay()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            state = state == LabelerState.Coordinates ? LabelerState.O : LabelerState.Coordinates;
        }
    }

    void SetLabelColor()
    {
        if (gridManager == null) return;

        Node node = gridManager.GetNode(coordinates);

        if (node == null) return;

        if (node.isSelected)
        {
            label.color = selectedColor;
        }
        else if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void ProcessLabels()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        switch (state)
        {
            case LabelerState.Coordinates:
                label.text = $"{coordinates.x},{coordinates.y}";
                break;

            case LabelerState.O:
                label.text = "O";
                break;
            default:
                break;
        }
        

    }
    void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
