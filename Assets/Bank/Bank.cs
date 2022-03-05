using System;
using System.Collections;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public int CurrentBalance { get { return currentBalance; } }
    public int CurrentObstacleCount { get { return currentObstacleCount; } }
    public int CurrentHitpoints{ get { return currentHitpoints; } }

    [SerializeField] int startingBalance = 150;
    [SerializeField] int startingObstacleCount = 10;
    [SerializeField] int startingHitpoints = 10;
    [SerializeField] HealthBar playerHealthBar;

    BankMenuController bankMenu;
    GameManager game;

    int currentBalance;
    int currentObstacleCount;
    int currentHitpoints;

    void Awake()
    {
        bankMenu = FindObjectOfType<BankMenuController>();
        game = FindObjectOfType<GameManager>();
        
        currentBalance = startingBalance;
        currentObstacleCount = startingObstacleCount;
        currentHitpoints = startingHitpoints;
        playerHealthBar.SetMaxHealth(startingHitpoints);
    }

    public void DepositBalance(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }

    public bool WithdrawBalance(int amount)
    {
        if (currentBalance < amount) return false;

        currentBalance -= Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
        return true;
    }

    public void IncreaseObstacleCount(int amount)
    {
        currentObstacleCount += Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }

    public bool DecreaseObstacleCount(int amount)
    {
        if (currentObstacleCount < amount) return false;

        currentObstacleCount -= Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
        return true;
    }

    public void DecreaseHitpoints(int amount)
    {
        currentHitpoints -= Mathf.Abs(amount);
        playerHealthBar.SetHealth(currentHitpoints);

        if (currentHitpoints <= 0)
        {
            game.ProcessLoss();
        }
    }
}
