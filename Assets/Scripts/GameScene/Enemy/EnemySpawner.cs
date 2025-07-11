using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy & Spawn Settings")]
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private float initialSpawnInterval = 5f;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float spawnIntervalDecrease = 0.5f;
    [SerializeField] private int scoreStepToIncrease = 500;
    [SerializeField] private float destroyTime = 20f;

    private float currentSpawnInterval;
    private float spawnTimer;
    private int nextScoreThreshold;

    private PlayerMove player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        currentSpawnInterval = initialSpawnInterval;
        spawnTimer = currentSpawnInterval;
        nextScoreThreshold = scoreStepToIncrease;
    }

    private void Update()
    {
        if (player != null && player.receiveInput)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemiesAtAllPoints();
                spawnTimer = currentSpawnInterval;
            }

            CheckAndReduceSpawnInterval();
        }
    }

    private void CheckAndReduceSpawnInterval()
    {
        if (InGameUI.Instance != null && InGameUI.Instance.score >= nextScoreThreshold)
        {
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalDecrease);
            nextScoreThreshold += scoreStepToIncrease;
        }
    }

    private void SpawnEnemiesAtAllPoints()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            SpawnEnemyAtPoint(spawnPoint);
        }
    }

    private void SpawnEnemyAtPoint(GameObject spawnObject)
    {
        if (spawnObject == null) return;

        int childCount = spawnObject.transform.childCount;
        if (childCount == 0) return;

        Vector3 spawnPos = spawnObject.transform.GetChild(Random.Range(0, childCount)).position;
        GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        Destroy(enemy, destroyTime);
    }
}
