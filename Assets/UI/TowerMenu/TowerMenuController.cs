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

    Label towerName;
    Label towerInfo;
    Label towerStats;

    VisualElement root;
    Tower tower;
    Weapon towerWeapon;
    Bank bank;
    GridManager gridManager;
    Pathfinder pathfinder;

    bool isOpened = false;
    Vector2Int coordinates;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        bank = FindObjectOfType<Bank>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();

        QueryViewControls();
        SetOnEventHandlers();
        Hide();
        DisableButtons();
    }

    void DisableButtons()
    {
        towerOneBtn.SetEnabled(false);
        towerTwoBtn.SetEnabled(false);
        towerThreeBtn.SetEnabled(false);
    }


    public void SetOnEventHandlers()
    {
        towerOneBtn.clicked += TowerOneBtnPressed;
        towerTwoBtn.clicked += TowerTwoBtnPressed;
        towerThreeBtn.clicked += TowerThreeBtnPressed;
        sellBtn.clicked += SellBtnPressed;
        closeButton.clicked += Hide;
    }

    public void QueryViewControls()
    {
        towerOneBtn = root.Q<Button>("tower-1-btn");
        towerTwoBtn = root.Q<Button>("tower-2-btn");
        towerThreeBtn = root.Q<Button>("tower-3-btn");
        sellBtn = root.Q<Button>("sell-btn");
        closeButton = root.Q<Button>("close-btn");
        towerName = root.Q<Label>("tower-name");
        towerInfo = root.Q<Label>("tower-info");
        towerStats = root.Q<Label>("tower-stats");
    }

    public void ToggleVisibility(bool display)
    {
        isOpened = display;
        root.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void BindSelectedTower(Vector2Int coordinatesOfNode)
    {
        this.tower = gridManager.GetNode(coordinatesOfNode).placedTower;
        this.coordinates = coordinatesOfNode;
        towerWeapon = tower.GetComponent<Tower>().GetComponentInChildren<Weapon>();
        towerName.text = towerWeapon.Name;
        towerInfo.text = towerWeapon.Info;
        towerStats.text = towerWeapon.Stats;
    }

    void Hide()
    {
        ToggleVisibility(false);
    }

    void TowerOneBtnPressed()
    {
        tower.SetTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon1, tower, tower.transform.position);
    }

    void TowerTwoBtnPressed()
    {
        tower.SetTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon2, tower, tower.transform.position);
    }

    void TowerThreeBtnPressed()
    {
        tower.SetTowerWeapon(PrefabManager.PrefabIndices.TowerWeapon3, tower, tower.transform.position);
    }

    void SellBtnPressed()
    {
        // TODO: Still not working, need to update specific Tile on destruction
        Destroy(tower.gameObject);
        tower = null;
        gridManager.GetNode(coordinates).placedTower = null;
        gridManager.FreeNode(coordinates);
        gridManager.SelectNodeTile(new Vector2Int(-1,-1));
        pathfinder.BroadcastRecalculatePath();
    }
}