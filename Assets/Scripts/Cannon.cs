using UnityEngine;

public class Cannon : MonoBehaviour
{
    private GameObject gameController;
    private Score score;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        score = gameController.GetComponent<Score>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            OnFire();
    }

    private void OnFire()
    {
        Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition2D = new Vector2(mousePosition3D.x, mousePosition3D.y);
        
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition2D, Vector2.zero);
        
        if (hits.Length == 0)
        {
            score.TargetMissed();
            return;
        }

        foreach (RaycastHit2D hit in hits)
        {
            Collider2D collider = hit.collider;
            if (collider == null || !collider.gameObject.CompareTag("Target"))
                return;
            Target target = collider.GetComponent<Target>();
            target.HitTarget();
        }
    }
}
