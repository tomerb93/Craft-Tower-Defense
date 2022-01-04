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

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        bankMenu.UpdateDisplay();
    }
}
