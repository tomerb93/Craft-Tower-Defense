using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;

    Transform target;

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void AimWeapon()
    {
        weapon.LookAt(target);
    }

    void FindClosestTarget()
    {
        // get all game objects with EnemyMover script
        EnemyMover[] enemies = FindObjectsOfType<EnemyMover>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (EnemyMover enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }
}
