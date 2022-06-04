using Assets.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

public class TopMenuController : MonoBehaviour, IViewWithButton
{
    VisualElement root;
    Button pauseResumeBtn;
    GameManager game;

    void Awake()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        game = FindObjectOfType<GameManager>();


        QueryViewControls();
        SetOnEventHandlers();
        UpdateButtonText();
    }

    public void QueryViewControls()
    {
        pauseResumeBtn = root.Q<Button>("pause-resume-btn");
    }

    public void SetOnEventHandlers()
    {
        pauseResumeBtn.clicked += PauseResumeLevel;
    }

    private void PauseResumeLevel()
    {
        if (game.IsPaused)
        {
            game.ResumeLevel();
            UpdateButtonText();
        }
        else
        {
            game.PauseLevel();
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        pauseResumeBtn.text = game.IsPaused ? "Resume" : "Pause";
    }
}
