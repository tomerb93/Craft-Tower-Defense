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
    bool isSlowed;

    void OnEnable()
    {
        ReturnToStart();
        CalculatePath(true);
    }

    void OnParticleCollision(GameObject other)
    {
        StartCoroutine(ProcessHit(other.GetComponentInParent<Weapon>()));
    }


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }
    IEnumerator ProcessHit(Weapon weapon)
    {
        if (weapon.HasSlow && !isSlowed)
        {
            Debug.Log("Slowed");
            speed -= weapon.Slow;
            isSlowed = true;

            yield return new WaitForSeconds(weapon.SlowDuration);

            Debug.Log("Slow Ended");
            speed += weapon.Slow;
            isSlowed = false;
        }
    }

    void CalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
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

        // disable instead of destroying
        gameObject.SetActive(false);
    }
    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }
}
