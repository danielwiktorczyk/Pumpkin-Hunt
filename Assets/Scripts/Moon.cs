using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    private GameObject gameController;
    private Level level;
    private float pathProgress = 0;
    private Vector3 initialPosition;
    private float archHeight = 4.0f;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        level = gameController.GetComponent<Level>();
        initialPosition = transform.position;
    }

    void Update()
    {
        float timeElapsed = Level.levelDuration - Level.timeLeftInLevel;
        float xPosition = initialPosition.x + (timeElapsed / Level.levelDuration * 2 * Mathf.Abs(initialPosition.x));
        float yPosition = initialPosition.y + archHeight - (archHeight / Mathf.Pow(Mathf.Abs(initialPosition.x), 2.0f)) * Mathf.Pow(xPosition, 2.0f);
        transform.position = new Vector3(
            xPosition,
            yPosition,
            initialPosition.z);
    }
}
