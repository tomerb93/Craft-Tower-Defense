using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BankMenuController : MonoBehaviour
{
    // TODO: Change TextFields to label and update text (Easier to edit display this way)
    TextField balance;
    TextField obstaclesLeft;

    Bank bank;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        bank = FindObjectOfType<Bank>();

        balance = root.Q<TextField>("balance");
        obstaclesLeft = root.Q<TextField>("obstacles-left");

        if (bank != null)
        {
            UpdateDisplay();
        }
    }

    public void UpdateDisplay()
    {
        balance.value = bank.CurrentBalance.ToString();
        obstaclesLeft.value = bank.CurrentObstacleCount.ToString();
    }
}
