using System;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    ParticleSystem bulletParticleSystem;
    Transform target;
    Weapon weapon;

    void Start()
    {
        SetWeapon();
    }

    void Update()
    {
        FindClosestTarget();
        if (target != null)
        {
            AimAndFireWeapon();
        }
        else
        {
            FireWeapon(false);
        }
    }

    public void SetWeapon()
    {
        weapon = GetComponentInChildren<Weapon>();
        bulletParticleSystem = weapon.GetComponentInChildren<ParticleSystem>();
    }

    void AimAndFireWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        
        if (targetDistance < weapon.Range)
        {
            weapon.transform.LookAt(target);
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
