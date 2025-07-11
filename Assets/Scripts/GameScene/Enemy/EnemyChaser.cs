using System.Collections;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float baseChaseSpeed = 5f;
    [SerializeField] private float followDistance = 5f;
    [SerializeField] private float verticalFollowSmoothness = 5f;
    [SerializeField] private GameObject enemyMsg;
    [SerializeField] private Transform msgPoint;
    private Rigidbody2D rb;

    [Header("Script References")]
    private InGameUI gameUI;
    private PlayerMove playerScript;
    private PlayerFollower follower; 

    private bool hasCollidedWithPlayer = false;
    private bool isFollowingPlayer ;

    private void Start()
    {
        isFollowingPlayer = true;
        followDistance = 110f;
        follower = FindFirstObjectByType<PlayerFollower>(); 
        gameUI = FindFirstObjectByType<InGameUI>();
        rb = GetComponent<Rigidbody2D>();
        playerScript = player.GetComponent<PlayerMove>();
    }

    void Update()
    {
        followPlayer();


    }
    private void followPlayer()
    {
        if (hasCollidedWithPlayer)
            return; // Stop moving after collision

        float targetY = Mathf.Lerp(transform.position.y, player.position.y, verticalFollowSmoothness * Time.deltaTime);

        if (gameUI.playerCollidedWithEnemy && playerScript.speed == 0)
        {
            // Move forward to catch player
            float newX = transform.position.x + baseChaseSpeed * Time.deltaTime;
            transform.position = new Vector2(newX, targetY);
        }
        else if (playerScript.receiveInput && follower.followPlayer )
        {
            followDistance = 35f;
            float targetX = player.position.x - followDistance;

            // Smoothly interpolate to the target X position
            float smoothedX = Mathf.Lerp(transform.position.x, targetX, 5f * Time.deltaTime);
            transform.position = new Vector2(smoothedX, targetY);
        }
    }
   
    public void enemiesMsg()
    {
        GameObject msg = Instantiate(enemyMsg, msgPoint.position, Quaternion.identity);
        Destroy(msg , 2f );
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            hasCollidedWithPlayer = true;
            rb.linearVelocity = Vector2.zero;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player")) hasCollidedWithPlayer = false;
    }

}
