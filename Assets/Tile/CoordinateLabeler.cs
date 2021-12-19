using TMPro;
using UnityEngine;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    Color defaultColor = Color.white;
    Color blockedColor = Color.gray;
    Color exploredColor = Color.yellow;
    Color pathColor = Color.red;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        ProcessLabels();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            ProcessLabels();
            UpdateTileName();
        }

        SetLabelColor();
        ToggleLabelsDisplay();
    }

    void ToggleLabelsDisplay()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if (gridManager == null) return;

        Node node = gridManager.GetNode(coordinates);

        if (node == null) return;

        if (!node.isWalkable)
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

        label.text = $"{coordinates.x},{coordinates.y}";
    }
    void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
