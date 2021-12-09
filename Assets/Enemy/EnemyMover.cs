using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] float speed = 1f;

    List<Node> path = new List<Node>();

    GridManager gridManager;
    Pathfinder pathfinder;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }


    void Start()
    {
        GetPath();
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        Debug.Log("Starting path");
        for(int i = 0; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    void GetPath()
    {
        Debug.Log("Getting path");
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath();
    }
}
