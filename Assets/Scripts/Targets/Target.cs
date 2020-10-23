﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject gameController;
    private Score score;
    [SerializeField]
    private int pointWorth = 3;
    [SerializeField]
    private int objectiveWorth = 1;
    [SerializeField]
    private int shotsRequired = 1;
    private bool hitThisFrame = false;
    private AudioSource audioSource;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        score = gameController.GetComponent<Score>();
        StartCoroutine(OutOfBoundsCheckCoRoutine());
        audioSource = GetComponentInChildren<AudioSource>();
    }

    void Update()
    {
        hitThisFrame = false;
    }

    public virtual void HitTarget()
    {
        if (hitThisFrame)
            return;

        shotsRequired -= 1;
        if (shotsRequired <= 0 || Level.isSpecialMode)
            DestroyTarget();
        hitThisFrame = true;

        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
    }

    private void DestroyTarget()
    {
        score.TargetDestroyed(pointWorth, objectiveWorth, transform.position);
        AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
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
