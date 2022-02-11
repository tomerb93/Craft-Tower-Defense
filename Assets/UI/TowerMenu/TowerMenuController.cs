using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenuController : MonoBehaviour
{
    public bool IsOpened { get { return isOpened; } }

    [SerializeField] int startingAttackCost = 20;
    [SerializeField] int startingSpeedCost = 10;
    [SerializeField] int startingSlowCost = 50;

    Button addAttackButton;
    Button addSpeedButton;
    Button addSlowButton;
    Button closeButton;

    VisualElement root;
    Weapon towerWeapon;
    Bank bank;

    int currentAttackCost;
    int currentSpeedCost;
    int currentSlowCost;

    bool isOpened = false;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        bank = FindObjectOfType<Bank>();

        currentAttackCost = startingAttackCost;
        currentSpeedCost = startingSpeedCost;
        currentSlowCost = startingSlowCost;

        QueryViewControls();
        SetButtonText();
        SetOnEventHandlers();
        Hide();
    }

    void SetButtonText()
    {
        addAttackButton.text = "Attack, " + currentAttackCost;
        addSpeedButton.text = "Speed, " + currentSpeedCost;
        addSlowButton.text = "Slow, " + currentSlowCost;
    }

    void SetOnEventHandlers()
    {
        addAttackButton.clicked += AddAttackButtonPressed;
        addSpeedButton.clicked += AddSpeedButtonPressed;
        addSlowButton.clicked += AddSlowButtonPressed;
        closeButton.clicked += Hide;
    }

    void QueryViewControls()
    {
        addAttackButton = root.Q<Button>("add-attack-button");
        addSpeedButton = root.Q<Button>("add-speed-button");
        addSlowButton = root.Q<Button>("add-slow-button");
        closeButton = root.Q<Button>("close-button");
    }

    public void ToggleVisibility(bool display)
    {
        isOpened = display;
        root.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void BindSelectedTower(Tower tower)
    {
        towerWeapon = tower.GetComponentInChildren<Weapon>();
    }

    void Hide()
    {
        ToggleVisibility(false);
    }

    void AddAttackButtonPressed()
    {
        if (bank.WithdrawBalance(currentAttackCost))
        {
            towerWeapon.UpgradeDamage(0.25f);
            currentAttackCost += 2;
            SetButtonText();
        }
    }

    void AddSpeedButtonPressed()
    {
        if (bank.WithdrawBalance(currentSpeedCost))
        {
            towerWeapon.UpgradeSpeed(0.5f);
            currentSpeedCost++;
            SetButtonText();
        }
    }

    void AddSlowButtonPressed()
    {
        if (bank.WithdrawBalance(currentSlowCost))
        {
            towerWeapon.UpgradeSlow(0.05f);
            currentSlowCost += 5;
            SetButtonText();
        }
        
    }
}
