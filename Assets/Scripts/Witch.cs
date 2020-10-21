using UnityEngine;

public class Witch : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private Vector2 direction = Vector2.left;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * direction);
    }


}
