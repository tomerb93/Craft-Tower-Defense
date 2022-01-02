using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenuController : MonoBehaviour
{
    Button upgradeAttack;
    VisualElement root;
    Weapon towerWeapon;


    // Start is called before the first frame update
    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        upgradeAttack = root.Q<Button>("upg-attack-button");

        upgradeAttack.clicked += UpgradeAttackButtonPressed;

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

    void UpgradeAttackButtonPressed()
    {
        towerWeapon.AddDamage(1f);
    }
}
