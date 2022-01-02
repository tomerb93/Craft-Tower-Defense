using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenuController : MonoBehaviour
{
    public bool IsOpened { get { return isOpened; } }

    Button addAttackButton;
    Button addSpeedButton;
    Button addSlowButton;
    Button closeButton;

    VisualElement root;
    Weapon towerWeapon;

    bool isOpened = false;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        addAttackButton = root.Q<Button>("add-attack-button");
        addSpeedButton = root.Q<Button>("add-speed-button");
        addSlowButton = root.Q<Button>("add-slow-button");
        closeButton = root.Q<Button>("close-button");

        addAttackButton.clicked += AddAttackButtonPressed;
        addSpeedButton.clicked += AddSpeedButtonPressed;
        addSlowButton.clicked += AddSlowButtonPressed;
        closeButton.clicked += HideWindow;

        ToggleVisibility(false);
    }


    public void ToggleVisibility(bool shouldDisplay)
    {
        isOpened = shouldDisplay;
        root.style.display = shouldDisplay ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void BindSelectedTower(GameObject tower)
    {
        towerWeapon = tower.GetComponentInChildren<Weapon>();
    }

    void HideWindow()
    {
        ToggleVisibility(false);
    }

    void AddAttackButtonPressed()
    {
        towerWeapon.AddDamage(1f);
    }

    void AddSpeedButtonPressed()
    {
        towerWeapon.AddSpeed(0.5f);
    }

    void AddSlowButtonPressed()
    {
        towerWeapon.AddSlow(0.05f);
    }

    public enum State
    {
        OPENED,
        CLOSED
    }
}
