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
    Label towerLevel;

    VisualElement root;
    Tower tower;
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

    void Update()
    {
        if (isOpened)
        {
            ProcessInput();
        }
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Alpha1) )
        {
            TowerOneBtnPressed();
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            TowerTwoBtnPressed();
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            TowerThreeBtnPressed();
        }

        if (Input.GetKey(KeyCode.F))
        {
            MergeTower();
        }

        if (Input.GetKey(KeyCode.S))
        {
            SellBtnPressed();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Hide();
        }
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
        towerLevel = root.Q<Label>("tower-level");
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
        RefreshWeaponText();
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

    void RefreshWeaponText()
    {
        var towerWeapon = tower.GetComponentInChildren<Weapon>();
        towerName.text = towerWeapon.Name;
        towerInfo.text = towerWeapon.Info;
        towerStats.text = towerWeapon.Stats;
        towerLevel.text = $"Level:{tower.PowerLevel}";
        mergeButton.text = $"Fuse (F) - {towerWeapon.GetMergeCost()}";
    }

    void TowerOneBtnPressed()
    {
        tower.BuildTowerWeapon(PrefabManager.PrefabIndices.RifleTower);
        RefreshWeaponText();
    }

    void TowerTwoBtnPressed()
    {
        tower.BuildTowerWeapon(PrefabManager.PrefabIndices.GoopTower);
        RefreshWeaponText();
    }

    void TowerThreeBtnPressed()
    {
        tower.BuildTowerWeapon(PrefabManager.PrefabIndices.CryogenicTower);
        RefreshWeaponText();
    }

    void SellBtnPressed()
    {
        bank.DepositBalance(tower.Value);
        gridManager.DestroyTowerAndRemoveFromNode(tower.Coordinates);
        ToggleVisibility(false);
        
    }

    void MergeTower()
    {
        tower.BeginFusion();
        ToggleVisibility(false);
    }
}