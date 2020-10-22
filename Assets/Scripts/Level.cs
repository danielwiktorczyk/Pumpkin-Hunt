using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private GameObject gameController;
    private Cannon cannon;    
    private Score score;
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
    private int initialTargets = 0;
    [SerializeField]
    public float speed = 1.0f;
    [SerializeField]
    public float levelDuration = 20.0f;
    public float timeLeftInLevel;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        cannon = gameController.GetComponent<Cannon>();
        score = gameController.GetComponent<Score>();
        
        initialTargets = RemainingTargets();
        timeLeftInLevel = levelDuration;
    }

    void Update()
    {
        TargetsLeft();
        //ShotsLeft();
        TimeLeftInLevel();
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
        targetsLeftText.text = $"Objective: {Score.objectivesDestroyedInLevel} / {targetObjective}";
    }

    private int RemainingTargets()
    {
        return GameObject.FindGameObjectsWithTag("Target").Length;
    }
}
