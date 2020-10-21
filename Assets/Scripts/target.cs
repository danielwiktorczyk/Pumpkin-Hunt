using UnityEngine;


//public class Witch : Target
//{

//}
public class Target : MonoBehaviour
{
    private GameObject gameController;
    private Score score;
    [SerializeField]
    private int pointWorth = 3;
    [SerializeField]
    private int shotsRequired = 1;
    //[SerializeField]
    //private TargetMovement targetMovement; // WitchMovement : TargetMovement

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        score = gameController.GetComponent<Score>();
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * 0.1f * Vector2.up);
        //* from current position A, move to position B in Xseconds, in a sinuloadal way 

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
