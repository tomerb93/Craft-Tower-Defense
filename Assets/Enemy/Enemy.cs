using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool IsDead { get; set; } = false;

    [SerializeField] int balanceReward = 25;
    [SerializeField] int hitpointPenalty = 1;
    [SerializeField] ParticleSystem spawnParticleSystem;

    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
        if (!spawnParticleSystem.isPlaying)
        {
            spawnParticleSystem.Play();
        }
    }

    public void RewardBalance()
    {
        if (bank == null) return;

        bank.DepositBalance(balanceReward);
    }

    public void StealHitpoints()
    {
        if (bank == null) return;

        bank.DecreaseHitpoints(hitpointPenalty);
    }
}