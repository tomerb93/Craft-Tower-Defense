using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] float reloadOnLossTimer = 2f;

    AlertController alert;
    public bool IsPaused => isPaused;

    private bool isPaused;
    int mainMenuIndex = 0;
    bool isLosing = false;

    void Awake()
    {
        alert = FindObjectOfType<AlertController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ReloadLevel();
        }

        if (Input.GetKey(KeyCode.N))
        {
            LoadNextLevel();
        }
    }

    public void PauseLevel()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeLevel()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void ReloadLevel()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void LoadNextLevel()
    {
        // TODO: If last level, loop back to first level for now - change this to victory screen
        int nextSceneBuildIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneBuildIndex);
    }

    public void ProcessLoss()
    {
        if (!isLosing)
        {
            StartCoroutine(ProcessLossRequest());
        }
        
    }

    IEnumerator ProcessLossRequest()
    {
        isLosing = true;

        alert.Alert("YOU LOSE", 24, true);

        yield return new WaitForSeconds(reloadOnLossTimer);

        LoadMainMenu();

        isLosing = false;
    }
}