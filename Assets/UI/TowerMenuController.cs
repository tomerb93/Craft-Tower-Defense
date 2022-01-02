using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenuController : MonoBehaviour
{
    Button addAttackButton;
    Button addSpeedButton;
    Button addSlowButton;
    Button closeButton;

    VisualElement root;
    Weapon towerWeapon;

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

        SetVisibility(false);
    }


    public void SetVisibility(bool shouldDisplay)
    {
        root.style.display = shouldDisplay ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void BindSelectedTower(GameObject tower)
    {
        towerWeapon = tower.GetComponentInChildren<Weapon>();

        Debug.Log("Tower is :" + tower);
        Debug.Log("Tower weapon is " + towerWeapon);
    }

    void HideWindow()
    {
        SetVisibility(false);
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
}
