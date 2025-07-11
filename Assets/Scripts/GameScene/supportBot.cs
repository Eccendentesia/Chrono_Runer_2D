using UnityEngine;

public class SupportBot : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float attackRangeX = 5f, attackRangeY = 5f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float moveSpeed = 5f;
    Vector3 distance = new Vector3(5f, 0f, 0f);

    private float attackTimer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        FollowPlayer();
        AttackIfEnemyInRange();
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position - distance, moveSpeed * Time.deltaTime);
        }
    }

    private void AttackIfEnemyInRange()
    {
        attackTimer -= Time.deltaTime;

        Collider2D[] enemiesInRange = Physics2D.OverlapBoxAll(transform.position, new Vector2(attackRangeX, attackRangeY), 0f, enemyLayer);

        if (enemiesInRange.Length > 0 && attackTimer <= 0f)
        {
            Transform targetEnemy = enemiesInRange[0].transform;

            GameObject bulletObj = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Bullet bullet = bulletObj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetTarget(targetEnemy);
            }

            attackTimer = attackCooldown;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(attackRangeX, attackRangeY, 1));
    }
}
