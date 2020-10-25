using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour
{
    private GameObject gameController;
    private Level level;
    //private float fade = 0;
    //private float angle = 0;
    //private Material originalMaterial;
    private Vector2 destination;
    [SerializeField]
    private int desinationCount = 5;
    private int desinationsVisited = 0;
    private float destinationTimeProgress = 0;
    private float destinationTime = 0;



    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        level = gameController.GetComponent<Level>();
        //originalMaterial = gameObject.GetComponent<Ren>().material;
        //Color originalColor = originalMaterial.color;
        //originalMaterial.SetColor("_Color", new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f));
        GenerateNewDestination();
    }

    private void GenerateNewDestination()
    {
        if (desinationsVisited >= desinationCount)
        {
            destination = 1000 * Random.insideUnitCircle; // leaves this realm
            return;
        }

        destination = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-5.0f, 5.0f));
        desinationsVisited += 1;
        destinationTime = Random.Range(2.0f, 3.0f);
        destinationTimeProgress = 0;
    }

    void Update()
    {
        if (Level.isGamePaused)
            return;


        destinationTime += Time.deltaTime;

        //fade = (float) Math.Sin(angle) + 0.5f;
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(
            position, 
            destination, 
            Time.deltaTime * level.speed);
        if (position == destination || destinationTimeProgress > destinationTime)
            GenerateNewDestination();
    }

    public void SetNumberOfDestinations(int desinationCount)
    {
        this.desinationCount = desinationCount;
    }
}
