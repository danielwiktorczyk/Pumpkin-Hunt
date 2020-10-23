using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private GameObject gameController;
    private Cannon cannon;    
    [SerializeField]
    private Text targetsLeftText;
    [SerializeField]
    private Text shotsLeftText;
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

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        cannon = gameController.GetComponent<Cannon>();
        
        timeLeftInLevel = levelDuration;
    }

    void Update()
    {
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
        Debug.Log("SpecialMode");
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
                Debug.Log("Time's Up! Did not meet objective....");
                Application.Quit();
            } else
            {
                Debug.Log("Time's Up! Objective met! Next level");
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
        Debug.Log("?");
        targetsLeftText.text = $"{Score.objectivesDestroyedInLevel} / {targetObjective}";
    }
}
