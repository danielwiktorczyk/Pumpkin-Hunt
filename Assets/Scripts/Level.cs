using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Level : MonoBehaviour
{
    private GameObject gameController;
    private Cannon cannon;    
    [SerializeField]
    private Text targetsLeftText;
    [SerializeField]
    private Text shotsLeftText;
    [SerializeField]
    private Text roundsText;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    public int currentLevel = 1;
    [SerializeField]
    private int targetObjective;
    [SerializeField]
    private int shotLimit;
    [SerializeField]
    public float speed = 1.0f;
    [SerializeField]
    public static float levelDuration = 20.0f;
    public static float timeLeftInLevel;
    public static bool isSpecialMode = false;
    private static float specialModeCooldownProgress = 0;
    [SerializeField]
    private static float specialModeCooldown = 10.0f;
    public bool isInBetweenLevels;
    public float timeBetweenLevels = 5.0f;
    public float timeLeftBetweenLevels = 0;
    public static bool isGamePaused;

    void Start()
    {
        isGamePaused = true;
        Time.timeScale = 0;
        gameController = GameObject.FindGameObjectWithTag("GameController");
        cannon = gameController.GetComponent<Cannon>();
        
        roundsText.text = $"Round: {currentLevel}";

        timeLeftInLevel = levelDuration;
    }

    public void StartGame()
    {
        UnityEngine.Cursor.visible = false;
        startMenu.SetActive(false);
        isGamePaused = false;
        Time.timeScale = 1.0f;
    }


    private void PauseGame()
    {
        UnityEngine.Cursor.visible = true;
        isGamePaused = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void ResumeGame()
    {
        UnityEngine.Cursor.visible = false;
        isGamePaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
                ResumeGame();
            else
            {
                PauseGame();
                return;
            }
        }


        if (isGamePaused)
        {
            return;
        }

        specialModeCooldownProgress -= Time.deltaTime;
        if (specialModeCooldownProgress <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
                InvokeSpecialMode();
            else {
                specialModeCooldownProgress = 0;
                isSpecialMode = false;
            }
           
        }
        
        TargetsLeft();
        //ShotsLeft();
        TimeLeftInLevel();
    }

    private void InvokeSpecialMode()
    {
        isSpecialMode = true;
        specialModeCooldownProgress = specialModeCooldown;
    }

    private void TimeLeftInLevel()
    {
        timeLeftInLevel -= Time.deltaTime;
        if(timeLeftInLevel <= 0)
        {
            if (Score.objectivesDestroyedInLevel < targetObjective)
            {
                Application.Quit();
            } else
            {
                NewLevel();
            }
        }
    }

    private void NewLevel()
    {
        currentLevel += 1;
        speed = 1.0f + (currentLevel - 1) * 1.0f;
        timeLeftInLevel = levelDuration;

        timeLeftBetweenLevels = timeBetweenLevels;

        roundsText.text = $"Round: {currentLevel}";

        Score.NewLevel();
        Cannon.NewLevel();
    }

    private void ShotsLeft()
    {
        // TODO lose if shots taken >= shot limit
        shotsLeftText.text =
                    $"Shots Left: {shotLimit - cannon.ShotsTaken()}";
    }

    private void TargetsLeft()
    {
        targetsLeftText.text = $"{Score.objectivesDestroyedInLevel} / {targetObjective}";
    }
}
