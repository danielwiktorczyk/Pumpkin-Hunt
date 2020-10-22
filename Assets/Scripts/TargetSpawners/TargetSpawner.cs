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
    protected bool active = true;
    [SerializeField]
    protected int targetsToSpawnAtOnce = 1;
    [SerializeField]
    protected int targetsToSpawnAtOnceSpecial = 5;
    private float spawnCooldownProgress = 0;
    private float spawnCooldownProgressSpecial = 0;
    [SerializeField]
    protected float spawnCooldown = 5.0f;
    [SerializeField]
    protected float spawnCooldownSpecial = 0.0005f;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        level = gameController.GetComponent<Level>();
    }

    protected virtual void Update()
    {
        if (!active)
            return;

        UpdateCooldowns();

        if (Level.isSpecialMode)
            SpawnSpecial();
        else
            SpawnNormal();

    }

    private void UpdateCooldowns()
    {
        spawnCooldownProgress -= Time.deltaTime;
        if (spawnCooldownProgress <= 0)
            spawnCooldownProgress = 0;
        spawnCooldownProgressSpecial -= Time.deltaTime;
        if (spawnCooldownProgressSpecial <= 0)
            spawnCooldownProgressSpecial = 0;
    }

    private void SpawnNormal()
    {
        if (spawnCooldownProgress <= 0)
        {
            for (int i = 0; i < targetsToSpawnAtOnce; ++i)
                SpawnTarget();
            spawnCooldownProgress = spawnCooldown;
        }
    }

    private void SpawnSpecial()
    {
        if (spawnCooldownProgressSpecial <= 0)
        {
            for (int i = 0; i < targetsToSpawnAtOnceSpecial; ++i)
                SpawnTarget();
            spawnCooldownProgressSpecial = spawnCooldownSpecial;
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
