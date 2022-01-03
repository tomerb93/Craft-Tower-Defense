using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BankMenuController : MonoBehaviour
{
    TextField balance;
    TextField towersLeft;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        Bank bank = FindObjectOfType<Bank>();

        balance = root.Q<TextField>("balance");
        towersLeft = root.Q<TextField>("towers-left");

        if (bank != null)
        {
            balance.value = bank.CurrentBalance.ToString();
            towersLeft.value = bank.CurrentTowersLeft.ToString();
        }
    }
}
