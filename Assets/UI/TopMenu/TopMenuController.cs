using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class TopMenuController : MonoBehaviour, IViewWithButton
{
    [SerializeField] Sprite pauseSprite;
    [SerializeField] Sprite playSprite;

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
