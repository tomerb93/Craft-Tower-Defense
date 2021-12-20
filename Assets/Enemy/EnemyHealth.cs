using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHitPoints = 3;

    float currentHitPoints;

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit(other.GetComponentInParent<Weapon>());
    }

    void ProcessHit(Weapon weapon)
    {
        Debug.Log($"{weapon.Name} has done {weapon.Damage}");

        currentHitPoints -= weapon.Damage;

        if (currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
