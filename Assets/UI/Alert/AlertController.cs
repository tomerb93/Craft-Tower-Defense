using System;
using System.Collections;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;


// TODO: Create base interface/abstract class ViewController
// that declares repeating methods
public class AlertController : MonoBehaviour, IView
{
    Label alert;
    VisualElement root;
    float showTimer = 3f;

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

        StartCoroutine(ProcessAlertRequest());

    }

    IEnumerator ProcessAlertRequest()
    {
        root.style.display = DisplayStyle.Flex;

        yield return new WaitForSeconds(showTimer);

        root.style.display = DisplayStyle.None;
    }

    public void QueryViewControls()
    {
        alert = root.Q<Label>("alert-label");
    }
}
