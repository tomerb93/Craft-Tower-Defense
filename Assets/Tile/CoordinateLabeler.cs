using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private enum LabelerState
    {
        COORDINATES,
        X
    }

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    Color defaultColor = Color.white;
    Color blockedColor = Color.clear;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.red;
    Color selectedColor = Color.green;

    LabelerState state;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        state = LabelerState.X;
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
            state = state == LabelerState.COORDINATES ? LabelerState.X : LabelerState.COORDINATES;
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
            case LabelerState.COORDINATES:
                label.text = $"{coordinates.x},{coordinates.y}";
                break;

            case LabelerState.X:
                label.text = "X";
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
