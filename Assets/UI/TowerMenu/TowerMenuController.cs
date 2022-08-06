using System;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenuController : MonoBehaviour, IViewWithButton
{
    public bool IsOpened
    {
        get { return isOpened; }
    }

    public Button TowerOneBtn => towerOneBtn;
    public Button TowerTwoBtn => towerTwoBtn;
    public Button TowerThreeBtn => towerThreeBtn;

    Button towerOneBtn;
    Button towerTwoBtn;
    Button towerThreeBtn;
    Button sellBtn;
    Button closeButton;
    Button mergeButton;

    Label towerName;
    Label towerInfo;
    Label towerStats;

    VisualElement root;
    Tower tower;
    Weapon towerWeapon;
    Bank bank;
    GridManager gridManager;

    bool isOpened = false;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        bank = FindObjectOfType<Bank>();
        gridManager = FindObjectOfType<GridManager>();

        QueryViewControls();
        SetOnEventHandlers();
        Hide();
        DisableButtons();
    }

    

    public void QueryViewControls()
    {
        towerOneBtn = root.Q<Button>("tower-1-btn");
        towerTwoBtn = root.Q<Button>("tower-2-btn");
        towerThreeBtn = root.Q<Button>("tower-3-btn");
        mergeButton = root.Q<Button>("merge-btn");
        sellBtn = root.Q<Button>("sell-btn");
        closeButton = root.Q<Button>("close-btn");
        towerName = root.Q<Label>("tower-name");
        towerInfo = root.Q<Label>("tower-info");
        towerStats = root.Q<Label>("tower-stats");
    }

    public void SetOnEventHandlers()
    {
        towerOneBtn.clicked += TowerOneBtnPressed;
        towerTwoBtn.clicked += TowerTwoBtnPressed;
        towerThreeBtn.clicked += TowerThreeBtnPressed;
        sellBtn.clicked += SellBtnPressed;
        closeButton.clicked += Hide;
        mergeButton.clicked += MergeTower;
    }

    public void ToggleVisibility(bool display)
    {
        isOpened = display;
        root.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
        tower = null;
    }

    public void BindSelectedTower(Vector2Int coordinates)
    {
        this.tower = gridManager.GetNode(coordinates).placedTower;
        towerWeapon = tower.GetComponent<Tower>().GetComponentInChildren<Weapon>();
        towerName.text = towerWeapon.Name;
        towerInfo.text = towerWeapon.Info;
        towerStats.text = towerWeapon.Stats;
    }

    void Hide()
    {
        ToggleVisibility(false);
    }

    void DisableButtons()
    {
        towerOneBtn.SetEnabled(false);
        towerTwoBtn.SetEnabled(false);
        towerThreeBtn.SetEnabled(false);
    }

    void TowerOneBtnPressed()
    {
        tower.BuildTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon1);
    }

    void TowerTwoBtnPressed()
    {
        tower.BuildTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon2);
    }

    void TowerThreeBtnPressed()
    {
        tower.BuildTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon3);
    }

    void SellBtnPressed()
    {
        bank.DepositBalance(tower.Value);
        gridManager.DestroyTowerAndRemoveFromNode(tower.Coordinates);
        ToggleVisibility(false);
        
    }

    void MergeTower()
    {
        tower.GetComponent<Tower>().BeginMerge();
        ToggleVisibility(false);
    }
}