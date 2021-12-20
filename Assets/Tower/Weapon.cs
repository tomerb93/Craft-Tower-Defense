using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: MonoBehaviour
{
    public float Damage { get { return damage; } }
    public float Slow { get { return slow; } }
    public bool HasSlow { get { return slow > 0; } }
    public string Name { get { return weaponName; } }

    [SerializeField] float damage = 0.5f;
    [SerializeField] float speed = 4f;
    [SerializeField] string weaponName = "Weapon 1";
    [SerializeField] float slow = 0.1f;
    

    ParticleSystem bulletParticleSystem;

    void Awake()
    {
        bulletParticleSystem = GetComponentInChildren<ParticleSystem>();
    }

    void Start()
    {
        SetParticleSystemProperties();
    }

    void SetParticleSystemProperties()
    {
        var emission = bulletParticleSystem.emission;
        emission.rateOverTime = speed;
    }
}
