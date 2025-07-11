using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform pointA;    // Start position
    [SerializeField] private Transform pointB;    // End position
    [SerializeField] private float speed = 2f;  // Movement speed
    [SerializeField] private float size;

    private Transform target;

    void Start()
    {
        
        target = pointB; // Start by moving toward pointB
    }

    void Update()
    {
        // Move toward the target position
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // If reached target, switch to the other point
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            target = target == pointA ? pointB : pointA;
        }
        flip();
    }
    void flip()
    {
        if (target == pointA)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Face left
        }
        else
        {
            transform.rotation = Quaternion.Euler(0 , 180, 0); // Face left
        }
    }
}
