using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int balanceReward = 25;
    [SerializeField] int hitpointPenalty = 1;
    [SerializeField] ParticleSystem spawnParticleSystem;

    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
        if (spawnParticleSystem != null)
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
