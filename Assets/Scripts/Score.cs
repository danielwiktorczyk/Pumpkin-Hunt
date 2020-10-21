using System;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int scoreValue = 0;
    private int shotPoints = 0;
    private int targetsDestroyed = 0;
    [SerializeField]
    Text scoreText;

    void Start()
    {
    }

    void Update()
    {
        int bonusShotPoints = targetsDestroyed > 1 ? 5 : 0;
        scoreValue += shotPoints + bonusShotPoints;
        if (scoreValue < 0)
            scoreValue = 0;

        UpdateScoreText();

        shotPoints = 0;
        targetsDestroyed = 0;
    } 


    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {scoreValue}";
    }

    public void TargetDestroyed(int targetPointWorth)
    {
        shotPoints += targetPointWorth;
        targetsDestroyed += 1;
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
