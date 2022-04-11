using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftingMenuController : MonoBehaviour
{
    VisualElement root;
    Bank bank;
    bool isOpened = false;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        bank = FindObjectOfType<Bank>();
        ToggleVisibility(false);
    }

    void ToggleVisibility(bool display)
    {
        isOpened = display;
        root.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
