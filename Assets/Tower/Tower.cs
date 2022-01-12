using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 50;

    public Tower CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
        {
            return null;
        }

        if (bank.WithdrawBalance(cost))
        {
            return Instantiate(tower, position, Quaternion.identity);
        }

        return null;
    }
}
