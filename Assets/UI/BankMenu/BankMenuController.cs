using System.Collections;
using System.Collections.Generic;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

public class BankMenuController : MonoBehaviour, IView
{
    Label balance;
    Label obstaclesLeft;

    Bank bank;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();

        QueryViewControls();

        if (bank != null)
        {
            UpdateDisplay();
        }
    }

    public void UpdateDisplay()
    {
        balance.text = bank.CurrentBalance.ToString();
        obstaclesLeft.text = bank.CurrentObstacleCount.ToString();
    }

    public void QueryViewControls()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        balance = root.Q<Label>("balance");
        obstaclesLeft = root.Q<Label>("obs-left");
    }
}
