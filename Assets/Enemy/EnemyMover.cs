using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 10f)] float speed = 1f;
    [SerializeField] float offset = 2f;
    float currentSpeed = 1f;

    List<Node> path = new List<Node>();

    GridManager gridManager;
    Pathfinder pathfinder;
    Enemy enemy;
    bool isSlowed;

    void OnParticleCollision(GameObject other)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ProcessHit(other.GetComponentInParent<Weapon>()));
        }
    }

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();

        isSlowed = false;
        currentSpeed = speed;
        ReturnToStart();
        CalculatePath(true);
    }

    IEnumerator ProcessHit(Weapon weapon)
    {
        if (weapon.HasSlow && !isSlowed)
        {
            float oldSpeed = currentSpeed;
            currentSpeed *= (1f - weapon.Slow);
            isSlowed = true;

            yield return new WaitForSeconds(weapon.SlowDuration);

            currentSpeed = oldSpeed;
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

            endPosition = RandomizeEndPosition(endPosition, i);
            
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * currentSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    Vector3 RandomizeEndPosition(Vector3 endPosition, int i)
    {
        // Randomize enemy movement
        if (path[i - 1].coordinates.x != path[i].coordinates.x) // Direction is right/left, alter only z
        {
            endPosition.z += Random.Range(-offset, offset);
        }
        else
        {
            endPosition.x += Random.Range(-offset, offset);
        }

        return endPosition;
    }

    void FinishPath()
    {
        Destroy(gameObject);
        enemy.StealHitpoints();
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }
}
