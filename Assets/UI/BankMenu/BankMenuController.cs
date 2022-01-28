using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BankMenuController : MonoBehaviour
{
    // TODO: Change TextFields to label and update text (Easier to edit display this way)
    Label balance;
    Label obstaclesLeft;

    Bank bank;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        bank = FindObjectOfType<Bank>();

        balance = root.Q<Label>("balance");
        obstaclesLeft = root.Q<Label>("obs-left");

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
}
