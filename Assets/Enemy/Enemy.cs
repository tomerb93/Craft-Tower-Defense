using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int balanceReward = 25;
    [SerializeField] int hitpointPenalty = 1;

    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
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
