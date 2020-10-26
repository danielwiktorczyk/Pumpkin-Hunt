using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int scoreValue = 0;
    private static int shotPoints = 0;
    public static int targetsDestroyedOnShot = 0;
    public static int objectivesDestroyedInLevel = 0;
    public static int highscoreValue = 0;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text shotTextPrefab;
    private Vector3 shotPosition;
    private int bonusShotPoints = 0;
    private bool missedShot = false;

    public static void NewLevel()
    {
        shotPoints = 0;
        objectivesDestroyedInLevel = 0;
        targetsDestroyedOnShot = 0;
    }

    public void Update()
    {
        if (Level.isGamePaused)
            return;

        UpdatePoints();

        UpdateScoreText();
        RenderShotText();

        shotPoints = 0;
        targetsDestroyedOnShot = 0;
        bonusShotPoints = 0;
        missedShot = false;
    }

    private void UpdatePoints()
    {
        bonusShotPoints = targetsDestroyedOnShot > 1 ? 5 : 0;
        scoreValue += shotPoints + bonusShotPoints;
        if (scoreValue < 0)
            scoreValue = 0;
    }

    private void RenderShotText()
    {
        Text shotText;
        if (bonusShotPoints > 0)
        {
            shotText = Instantiate(shotTextPrefab, Camera.main.WorldToScreenPoint(shotPosition), Quaternion.identity);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            shotText.transform.SetParent(canvas.transform);
            shotText.color = Color.yellow;
            shotText.text = $"+{shotPoints + bonusShotPoints}!";
            shotText.tag = "ShotText";
            Destroy(shotText.gameObject, 1.0f);
        }
        else if (targetsDestroyedOnShot == 1 && shotPoints > 0)
        {
            shotText = Instantiate(shotTextPrefab, Camera.main.WorldToScreenPoint(shotPosition), Quaternion.identity);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            shotText.transform.SetParent(canvas.transform);
            shotText.color = Color.green;
            shotText.text = $"+{shotPoints}";
            shotText.tag = "ShotText";
            Destroy(shotText, 1.0f);
        }
        else if (missedShot)
        {
            shotText = Instantiate(shotTextPrefab, Camera.main.WorldToScreenPoint(shotPosition), Quaternion.identity);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            shotText.transform.SetParent(canvas.transform);
            shotText.color = Color.red;
            shotText.text = $"{shotPoints}";
            shotText.tag = "ShotText";
            Destroy(shotText, 1.0f);
        }       
        else if (targetsDestroyedOnShot == 1 && shotPoints == 0)
        {
            shotText = Instantiate(shotTextPrefab, Camera.main.WorldToScreenPoint(shotPosition), Quaternion.identity);
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
            shotText.transform.SetParent(canvas.transform);
            shotText.color = Color.grey;
            shotText.text = $"+{shotPoints}";
            shotText.tag = "ShotText";
            Destroy(shotText, 1.0f);
        }
    }

    public void Reset()
    {
        scoreValue = 0;
        shotPoints = 0;
        targetsDestroyedOnShot = 0;
        objectivesDestroyedInLevel = 0;
        bonusShotPoints = 0;
        missedShot = false;
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {scoreValue}";
    }

    public void TargetDestroyed(int targetPointWorth, int objectiveWorth, Vector3 position)
    {
        shotPoints += Level.isSpecialMode ? 1 : targetPointWorth;
        targetsDestroyedOnShot += 1;
        objectivesDestroyedInLevel += objectiveWorth;

        this.shotPosition = position;
    }

    public void TargetMissed(Vector3 position)
    {
        shotPoints = Level.isSpecialMode ? -2 : -1;

        missedShot = true;

        this.shotPosition = position;
    }

    public int GetScore()
    {
        return scoreValue;
    }

}
