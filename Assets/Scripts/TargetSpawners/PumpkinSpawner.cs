using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class PumpkinSpawner : TargetSpawner
{

    protected override void SpawnTarget()
    {
        Vector3 spawnLocation = new Vector3(
            transform.position.x + Random.Range(-10.0f, 10.0f),
            transform.position.y,
            0);

        GameObject target = Instantiate(
            targetToSpawn, 
            spawnLocation, 
            Quaternion.identity);
        
        Vector2 targetPosition = new Vector2(
            target.transform.position.x,
            target.transform.position.y);
        float pumkinSpeedFactor = 1.3f;
        float pumpkinGravity = pumkinSpeedFactor * 0.1f * level.speed;
        float launchSpeed = Random.Range(0.32f, 0.42f)
            * (float) Math.Sqrt(level.speed) * pumkinSpeedFactor;
        Vector2 launchTarget = new Vector2(
            Random.Range(-5.0f, 5.0f),
            5.0f);
        Vector2 launchVector = launchTarget - targetPosition;
        Rigidbody2D rb = target.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(launchSpeed * launchVector, ForceMode2D.Impulse);
        rb.gravityScale = pumpkinGravity;
    }
}
