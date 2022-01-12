using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


// TODO: Create base interface/abstract class ViewController
// that declares repeating methods
public class AlertController : MonoBehaviour
{
    Label alert;
    VisualElement root;
    float showTimer = 3f;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        alert = root.Q<Label>("alert-label");

        ToggleVisibility(false);
    }

    void ToggleVisibility(bool display)
    {
        root.style.display = display ? DisplayStyle.Flex : DisplayStyle.None;
    }

    public void Alert(string text)
    {
        // TODO: Implement fade in/out effect
        alert.text = text;
        StartCoroutine(ProcessAlertRequest());

    }

    IEnumerator ProcessAlertRequest()
    {
        root.style.display = DisplayStyle.Flex;

        yield return new WaitForSeconds(showTimer);

        root.style.display = DisplayStyle.None;
    }
}
