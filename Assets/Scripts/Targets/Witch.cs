using System;
using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private Vector2 direction = Vector2.right;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * direction);
    }

    public void SetDirection(bool right)
    {
        this.direction = right ? Vector2.right : Vector2.left;
        // TODO flip sprite as well!
    }
}
