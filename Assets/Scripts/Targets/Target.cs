using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject gameController;
    private Score score;
    private Level level;
    [SerializeField]
    private int pointWorth = 3;
    [SerializeField]
    private int objectiveWorth = 1;
    [SerializeField]
    private int shotsRequired = 1;
    private bool hitThisFrame = false;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        score = gameController.GetComponent<Score>();
        level = gameController.GetComponent<Level>();
        StartCoroutine(OutOfBoundsCheckCoRoutine());
    }

    private void Update()
    {
        hitThisFrame = false;
    }

    public virtual void HitTarget()
    {
        if (hitThisFrame)
            return;

        shotsRequired -= 1;
        if (shotsRequired <= 0)
            DestroyTarget();
        hitThisFrame = true;
    }

    private void DestroyTarget()
    {
        score.TargetDestroyed(pointWorth, objectiveWorth);
        Destroy(this.gameObject);
    }

    IEnumerator OutOfBoundsCheckCoRoutine()
    {
        while(true)
        {
            OutOfBoundsCheck();
            yield return new WaitForSeconds(1.0f);
        }
    }

    private void OutOfBoundsCheck()
    {
        if (Math.Abs(transform.position.x) > 15.0f
            || Math.Abs(transform.position.y) > 10.0f)
            Destroy(this.gameObject);
    }
}
