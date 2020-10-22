using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    protected GameObject gameController;
    public Level level;
    [SerializeField]
    protected GameObject targetToSpawn;
    [SerializeField]
    private bool active = true;
    [SerializeField]
    private int targetsToSpawnAtOnce = 1;
    private float spawnCooldownProgress = 0;
    private float spawnCooldown = 2.0f;
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        level = gameController.GetComponent<Level>();
    }

    void Update()
    {
        if (!active)
            return;
        
        spawnCooldownProgress -= Time.deltaTime;
        if (spawnCooldownProgress <= 0)
            spawnCooldownProgress = 0;

        if (spawnCooldownProgress <= 0)
        {
            for(int i = 0; i < targetsToSpawnAtOnce; ++i)
                SpawnTarget();
            spawnCooldownProgress = spawnCooldown;
        }
            
    }

    protected virtual void SpawnTarget()
    {
        Vector3 spawnLocation = new Vector3(
            transform.position.x + Random.Range(-10.0f, 10.0f),
            transform.position.y + Random.Range(-5.0f, 5.0f),
            0);

        GameObject target = Instantiate(targetToSpawn, spawnLocation, Quaternion.identity);
    }
}
