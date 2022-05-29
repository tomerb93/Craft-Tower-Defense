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

    public void ReloadLevel()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void ProcessLoss()
    {
        StartCoroutine(ProcessLossRequest());
    }

    IEnumerator ProcessLossRequest()
    {
        alert.Alert("You lose! Reloading level...", 24, true);

        yield return new WaitForSeconds(reloadOnLossTimer);

        ReloadLevel();
    }
}