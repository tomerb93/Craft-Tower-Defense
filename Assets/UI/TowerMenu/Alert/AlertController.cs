using System;
using System.Collections;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

public class AlertController : MonoBehaviour, IView
{
    Label alert;
    VisualElement root;
    float showTimer = 2f;
    bool isOpened = false;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        QueryViewControls();
        ToggleVisibility(false);
    }

    void ToggleVisibility(bool display)
    {
        root.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void Alert(string text, int fontSize = 16, bool bold = false)
    {
        alert.text = text;
        alert.style.fontSize = fontSize;
        alert.style.unityFontStyleAndWeight = bold ? FontStyle.Bold : FontStyle.Normal;

        if (!isOpened)
        {
            StartCoroutine(ProcessAlertRequest());
        }
    }

    IEnumerator ProcessAlertRequest()
    {
        root.style.display = DisplayStyle.Flex;
        isOpened = true;

        yield return new WaitForSeconds(showTimer);

        root.style.display = DisplayStyle.None;
        isOpened = false;
    }

    public void QueryViewControls()
    {
        alert = root.Q<Label>("alert-label");
    }
}