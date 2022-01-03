using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public int CurrentBalance { get { return currentBalance; } }
    public int CurrentTowersLeft { get { return currentTowersLeft; } }

    [SerializeField] int startingBalance = 150;
    [SerializeField] int startingTowerCount = 3;

    int currentBalance;
    int currentTowersLeft;

    void Awake()
    {
        currentBalance = startingBalance;
        currentTowersLeft = startingTowerCount;
    }
}
