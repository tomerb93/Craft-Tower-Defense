using System.Collections;
using System.Collections.Generic;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingMenuController : MonoBehaviour, IViewWithButton
{
    Button levelOneBtnOne;
    Button levelOneBtnTwo;

    VisualElement root;
    Bank bank;
    bool isOpened = false;
    TowerMenuController towerMenu;
    AlertController alert;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        bank = FindObjectOfType<Bank>();
        towerMenu = FindObjectOfType<TowerMenuController>();
        alert = FindObjectOfType<AlertController>();

        QueryViewControls();
        SetOnEventHandlers();
        ToggleVisibility(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleVisibility(!isOpened);
        }
    }

    void ToggleVisibility(bool display)
    {
        isOpened = display;
        root.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void QueryViewControls()
    {
        levelOneBtnOne = root.Q<Button>("cryo-tower-btn");
        levelOneBtnTwo = root.Q<Button>("goop-tower-btn");
    }

    public void SetOnEventHandlers()
    {
        levelOneBtnOne.clicked += LevelOneBtnOnePressed;
        levelOneBtnTwo.clicked += LevelOneBtnTwoPressed;
    }

    private void LevelOneBtnTwoPressed()
    {
        towerMenu.TowerTwoBtn.SetEnabled(true);
        alert.Alert("Purchased weapon: Goop Tower");
        levelOneBtnTwo.SetEnabled(false);
    }

    private void LevelOneBtnOnePressed()
    {
        towerMenu.TowerThreeBtn.SetEnabled(true);
        alert.Alert("Purchased weapon: Cryogenic Tower");
        levelOneBtnOne.SetEnabled(false);
    }
}