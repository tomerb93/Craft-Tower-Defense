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
        ToggleVisibility(false);
    }

    private void SetButtonText()
    {
        addAttackButton.text = addAttackButton.text + ", " + currentAttackCost;
        addSpeedButton.text = addSpeedButton.text + ", " + currentSpeedCost;
        addSlowButton.text = addSlowButton.text + ", " + currentSlowCost;
    }

    void SetOnEventHandlers()
    {
        addAttackButton.clicked += AddAttackButtonPressed;
        addSpeedButton.clicked += AddSpeedButtonPressed;
        addSlowButton.clicked += AddSlowButtonPressed;
        closeButton.clicked += HideWindow;
    }

    void QueryViewControls()
    {
        addAttackButton = root.Q<Button>("add-attack-button");
        addSpeedButton = root.Q<Button>("add-speed-button");
        addSlowButton = root.Q<Button>("add-slow-button");
        closeButton = root.Q<Button>("close-button");
    }

    public void ToggleVisibility(bool shouldDisplay)
    {
        isOpened = shouldDisplay;
        root.style.display = shouldDisplay ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void BindSelectedTower(Tower tower)
    {
        towerWeapon = tower.GetComponentInChildren<Weapon>();
    }

    void HideWindow()
    {
        ToggleVisibility(false);
    }

    void AddAttackButtonPressed()
    {
        towerWeapon.AddDamage(0.5f);
        bank.WithdrawBalance(currentAttackCost);
        currentAttackCost += 2;
    }

    void AddSpeedButtonPressed()
    {
        towerWeapon.AddSpeed(0.5f);
        bank.WithdrawBalance(currentSpeedCost);
        currentSpeedCost++;
    }

    void AddSlowButtonPressed()
    {
        towerWeapon.AddSlow(0.05f);
        bank.WithdrawBalance(currentSlowCost);
        currentSlowCost += 5;
    }
}
