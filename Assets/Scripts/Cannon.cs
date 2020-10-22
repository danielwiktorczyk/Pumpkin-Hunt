using System;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    private GameObject gameController;
    private Score score;
    private float remainingShootingCooldown = 0.0f;
    private float shootingCooldown = 0.5f;
    private float remainingSpecialShootingCooldown = 0;
    private float specialShootingCooldown = 0.20f;
    [SerializeField]
    GameObject crosshair;
    SpriteShapeRenderer[] crosshairSegments;
    private Color originalCrossHairColor;
    [SerializeField]
    private Text shootingCooldownText;
    [SerializeField]
    private GameObject scarecrowSpawner;
    private static int shotsTaken = 0;

    public static void NewLevel()
    {
        shotsTaken = 0;
    }
    
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        score = gameController.GetComponent<Score>();
        crosshair = GameObject.FindGameObjectWithTag("CrossHair");
        crosshairSegments = crosshair.GetComponentsInChildren<SpriteShapeRenderer>();
        originalCrossHairColor = crosshairSegments[0].color;
    }

    void Update()
    {
        UpdateCooldowns();
        UpdateCrossHairPosition();

        if (Level.isSpecialMode)
            CannonSpecial();
        else
            CannonNormal();
    }

    private void UpdateCrossHairPosition()
    {
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition2D = new Vector2(mousePosition3D.x, mousePosition3D.y);
        crosshair.transform.position = mousePosition2D;
    }

    private void UpdateCooldowns()
    {
        remainingShootingCooldown -= Time.deltaTime;
        if (remainingShootingCooldown < 0)
            remainingShootingCooldown = 0;
        remainingSpecialShootingCooldown -= Time.deltaTime;
        if (remainingSpecialShootingCooldown < 0)
            remainingSpecialShootingCooldown = 0;
    }

    private void CannonNormal()
    {
        if (Input.GetMouseButtonDown(0) && remainingShootingCooldown <= 0)
            OnFire();
        
        //shootingCooldownText.text = remainingShootingCooldown <= 0 ? "Fire at will!" : $"Reloading...";

        foreach(SpriteShapeRenderer renderer in crosshairSegments)
            renderer.color = remainingShootingCooldown <= 0 ? originalCrossHairColor : Color.grey;
    }

    private void CannonSpecial()
    {
        if (Level.isSpecialMode && Input.GetMouseButton(0) && remainingSpecialShootingCooldown <= 0)
            OnFire();
        
        //shootingCooldownText.text = "Special Mode Engaged! Fire at Will!";

        foreach (SpriteShapeRenderer renderer in crosshairSegments)
            renderer.color = Color.yellow;
    }


    private void OnFire()
    {
        shotsTaken += 1;

        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition2D = new Vector2(mousePosition3D.x, mousePosition3D.y);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
        
        if (hits.Length == 0)
        {
            score.TargetMissed();
            if(!Level.isSpecialMode)
                scarecrowSpawner.GetComponent<ScarecrowSpawner>().SummonScarecrow();
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

        remainingShootingCooldown = shootingCooldown;
        remainingSpecialShootingCooldown = specialShootingCooldown;
    }

    public int ShotsTaken()
    {
        return shotsTaken;
    }
}
