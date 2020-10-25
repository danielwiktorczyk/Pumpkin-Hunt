using System;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private GameObject gameController;
    private Level level;
    [SerializeField]
    private Vector2 direction = Vector2.right;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        level = gameController.GetComponent<Level>();
    }

    void Update()
    {
        transform.Translate(Time.deltaTime * (2.0f + Mathf.Sqrt(level.globalSpeed)) * direction);
    }

    public void SetDirection(bool right)
    {
        this.direction = right ? Vector2.right : Vector2.left;
        // TODO flip sprite as well!
    }
}
