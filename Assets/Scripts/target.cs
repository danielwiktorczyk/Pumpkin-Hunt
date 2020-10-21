using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject gameController;
    private Score score;
    private int pointWorth = 3;
    private int shotsRequired = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        score = gameController.GetComponent<Score>();
    }

    public void HitTarget()
    {
        shotsRequired -= 1;
        if (shotsRequired <= 0)
            DestroyTarget();
    }

    private void DestroyTarget()
    {
        score.TargetDestroyed(pointWorth);
        Destroy(this.gameObject);
    }
}
