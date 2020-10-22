using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour
{
    private GameObject gameController;
    private Score score;
    private float remainingShootingCooldown = 0.0f;
    private float shootingCooldown = 0.5f;
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
    }

    void Update()
    {
        remainingShootingCooldown -= Time.deltaTime;
        if (remainingShootingCooldown < 0)
            remainingShootingCooldown = 0;

        if (Input.GetMouseButtonDown(0) && remainingShootingCooldown <= 0)
            OnFire();

        shootingCooldownText.text = 
            remainingShootingCooldown <= 0 ? 
            "Fire at will!" : 
            $"Reloading...";
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
            scarecrowSpawner.GetComponent<ScarecrowSpawner>().SpawnScarecrow();
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
    }

    public int ShotsTaken()
    {
        return shotsTaken;
    }
}
