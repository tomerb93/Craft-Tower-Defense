using System;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weaponPrefab;
    [SerializeField] ParticleSystem bulletParticleSystem;

    Transform target;
    Weapon weapon;

    void Awake()
    {
        weapon = weaponPrefab.GetComponent<Weapon>();
    }

    void Update()
    {
        FindClosestTarget();
        AimAndFireWeapon();
    }

    void AimAndFireWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);


        if (targetDistance < weapon.Range)
        {
            weaponPrefab.LookAt(target);
            FireWeapon(true);
        }
        else
        {
            FireWeapon(false);
        }

    }

    private void FireWeapon(bool isActive)
    {
        var emissionModule = bulletParticleSystem.emission;
        emissionModule.enabled = isActive;
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
