﻿using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    private GameObject gameController;
    private Score score;
    private float shootingCooldown = 0.0f;
    [SerializeField]
    private Text shootingCooldownText;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        score = gameController.GetComponent<Score>();
    }

    void Update()
    {
        shootingCooldown -= Time.deltaTime;
        if (shootingCooldown < 0)
            shootingCooldown = 0;

        if (Input.GetMouseButtonDown(0) && shootingCooldown <= 0)
            OnFire();

        shootingCooldownText.text = 
            shootingCooldown <= 0 ? 
            "Fire at will!" : 
            $"Reloading...";
    }

    private void OnFire()
    {
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition2D = new Vector2(mousePosition3D.x, mousePosition3D.y);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
        
        if (hits.Length == 0)
        {
            score.TargetMissed();
        } else
        {
            foreach (RaycastHit2D hit in hits)
            {
                Collider2D collider = hit.collider;
                if (collider == null || !collider.gameObject.CompareTag("Target"))
                    return;
                Target target = collider.GetComponent<Target>();
                target.HitTarget();
            }
        }

        shootingCooldown = 1.0f;
    }
}
