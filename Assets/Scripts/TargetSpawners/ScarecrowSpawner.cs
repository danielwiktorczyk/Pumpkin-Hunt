using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowSpawner : TargetSpawner
{

    protected override void Update()
    {
        if (Level.isGamePaused)
            return;
    }

    protected override void SpawnTarget()
    {
        Vector3 spawnLocation = new Vector3(
            transform.position.x + Random.Range(-9.0f, 9.0f),
            transform.position.y,
            0);

        GameObject target = Instantiate(
            targetToSpawn,
            spawnLocation,
            Quaternion.identity);
    }

    public void SummonScarecrow()
    {
        SpawnTarget();
    }
}
