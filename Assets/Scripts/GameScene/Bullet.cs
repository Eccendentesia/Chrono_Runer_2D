using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionEffect;
    private Transform targetEnemy;
    private PlayerMove player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (targetEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetEnemy.position) < 0.1f)
        {
            HitTarget();
        }
    }

    public void SetTarget(Transform target)
    {
        targetEnemy = target;
    }

    private void HitTarget()
    {
        if (targetEnemy != null)
        {
            Destroy(targetEnemy.gameObject);

            if (explosionEffect != null)
            {
                GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.4f);
            }

            if (player != null && Settings.Instance.isPlayingSound)
            {
                player.explosionSound.PlayOneShot(player.explosionSound.clip);
            }
        }

        Destroy(gameObject);
    }
}
