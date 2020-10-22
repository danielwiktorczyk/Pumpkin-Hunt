using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static int scoreValue = 0;
    private static int shotPoints = 0;
    public static int targetsDestroyedOnShot = 0;
    public static int objectivesDestroyedInLevel = 0;
    [SerializeField]
    Text scoreText;

    void Start()
    {
    }

    public static void NewLevel()
    {
        shotPoints = 0;
        objectivesDestroyedInLevel = 0;
        targetsDestroyedOnShot = 0;
    }

    void Update()
    {
        int bonusShotPoints = targetsDestroyedOnShot > 1 ? 5 : 0;
        scoreValue += shotPoints + bonusShotPoints;
        if (scoreValue < 0)
            scoreValue = 0;

        UpdateScoreText();

        shotPoints = 0;
        targetsDestroyedOnShot = 0;
    } 


    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {scoreValue}";
    }

    public void TargetDestroyed(int targetPointWorth, int objectiveWorth)
    {
        shotPoints += targetPointWorth;
        targetsDestroyedOnShot += 1;
        objectivesDestroyedInLevel += objectiveWorth;
    }

    public void TargetMissed()
    {
        shotPoints = -1;
    }

    public int GetScore()
    {
        return scoreValue;
    }

}
