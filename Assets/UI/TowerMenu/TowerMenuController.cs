using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenuController : MonoBehaviour
{
    public bool IsOpened { get { return isOpened; } }

    [SerializeField] int startingAttackCost = 20;
    [SerializeField] int startingSpeedCost = 10;
    [SerializeField] int startingSlowCost = 50;

    Button towerOneBtn;
    Button towerTwoBtn;
    Button sellBtn;
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
        SetOnEventHandlers();
        Hide();

        towerOneBtn.SetEnabled(false);
    }


    void SetOnEventHandlers()
    {
        towerOneBtn.clicked += TowerOneBtnPressed;
        towerTwoBtn.clicked += TowerTwoBtnPressed;
        sellBtn.clicked += SellBtnPressed;
        closeButton.clicked += Hide;
    }

    void QueryViewControls()
    {
        towerOneBtn = root.Q<Button>("tower-1-btn");
        towerTwoBtn = root.Q<Button>("tower-2-btn");
        sellBtn = root.Q<Button>("sell-btn");
        closeButton = root.Q<Button>("close-btn");
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

    void TowerOneBtnPressed()
    {
        if (bank.WithdrawBalance(currentAttackCost))
        {
            towerWeapon.UpgradeDamage(0.25f);
            currentAttackCost += 2;
        }
    }

    void TowerTwoBtnPressed()
    {
        if (bank.WithdrawBalance(currentSpeedCost))
        {
            towerWeapon.UpgradeSpeed(0.5f);
            currentSpeedCost++;
        }
    }

    void SellBtnPressed()
    {
        if (bank.WithdrawBalance(currentSlowCost))
        {
            towerWeapon.UpgradeSlow(0.05f);
            currentSlowCost += 5;
        }
        
    }
}
