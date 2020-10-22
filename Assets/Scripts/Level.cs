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
    private int targetObjective;
    [SerializeField]
    private int shotLimit;
    private int initialTargets = 0;
    [SerializeField]
    public float speed = 1.0f;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        cannon = gameController.GetComponent<Cannon>();
        
        initialTargets = RemainingTargets();
    }

    void Update()
    {
        TargetsLeft();
        ShotsLeft();
    }

    private void ShotsLeft()
    {
        // TODO lose if shots taken >= shot limit
        shotsLeftText.text =
                    $"Shots Left: {shotLimit - cannon.ShotsTaken()}";
    }

    private void TargetsLeft()
    {
        targetsLeftText.text =
                    $"Objective: {initialTargets - RemainingTargets()} / {targetObjective}";
    }

    private int RemainingTargets()
    {
        return GameObject.FindGameObjectsWithTag("Target").Length;
    }
}
