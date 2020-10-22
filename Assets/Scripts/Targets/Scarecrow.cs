using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Scarecrow : MonoBehaviour
{
    private Vector2 restingPosition;

    void Start()
    {
        restingPosition = new Vector2(
            transform.position.x,
            transform.position.y + Random.Range(2.0f, 3.0f));

        SpriteShapeRenderer sprite = GetComponentInChildren<SpriteShapeRenderer>();
        sprite.color = new Color(
            sprite.color.r + Random.Range(-0.1f, 0.1f),
            sprite.color.g + Random.Range(-0.1f, 0.1f),
            sprite.color.b + Random.Range(-0.1f, 0.1f),
            sprite.color.a); 
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            restingPosition,
            Time.deltaTime);
    }
}
