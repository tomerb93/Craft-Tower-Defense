using UnityEngine;

public class Bank : MonoBehaviour
{
    public int CurrentBalance { get { return currentBalance; } }
    public int CurrentObstacleCount { get { return currentObstacleCount; } }

    [SerializeField] int startingBalance = 150;
    [SerializeField] int startingObstacleCount = 3;

    BankMenuController bankMenu;

    int currentBalance;
    int currentObstacleCount;

    void Awake()
    {
        bankMenu = FindObjectOfType<BankMenuController>();    
        
        currentBalance = startingBalance;
        currentObstacleCount = startingObstacleCount;
    }

    void Start()
    {
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
}
