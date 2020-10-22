using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchSpawner : TargetSpawner
{
    protected override void SpawnTarget()
    {
        Vector2 spawnLocation = 
            (1.5f * Random.insideUnitCircle) 
            + new Vector2(transform.position.x, transform.position.y);

        GameObject target = Instantiate(
           targetToSpawn,
           spawnLocation,
           Quaternion.identity);

        Witch witch = target.GetComponent<Witch>();
        witch.SetDirection(transform.right.x > 0);
    }
}
