using UnityEngine;

public class Bank : MonoBehaviour
{
    public int CurrentBalance { get { return currentBalance; } }
    public int CurrentObstacleCount { get { return currentObstacleCount; } }
    public int CurrentHitpoints{ get { return currentHitpoints; } }

    [SerializeField] int startingBalance = 150;
    [SerializeField] int startingObstacleCount = 10;
    [SerializeField] int startingHitpoints = 10;

    BankMenuController bankMenu;

    int currentBalance;
    int currentObstacleCount;
    int currentHitpoints;

    void Awake()
    {
        bankMenu = FindObjectOfType<BankMenuController>();    
        
        currentBalance = startingBalance;
        currentObstacleCount = startingObstacleCount;
        currentHitpoints = startingHitpoints;
    }

    public void DepositBalance(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }

    public void WithdrawBalance(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }

    public void IncreaseObstacleCount(int amount)
    {
        currentObstacleCount += Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }

    public void DecreaseObstacleCount(int amount)
    {
        currentObstacleCount -= Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }

    public void DecreaseHitpoints(int amount)
    {
        currentHitpoints -= Mathf.Abs(amount);
    }
}
