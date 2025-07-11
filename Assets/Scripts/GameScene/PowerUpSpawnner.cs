using UnityEngine;
using UnityEngine.Rendering;

public class PowerUpSpawnner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUps;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float powerUpInterval;
    [SerializeField] private float destroyTime;
    private float tempInterval;
    private PlayerMove player;
    void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        tempInterval = powerUpInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.receiveInput)
        {
            powerUpInterval -= Time.deltaTime;
            if (powerUpInterval <= 0)
            {
                spawn();
                powerUpInterval = tempInterval;
            }
        }
    }
    private void spawn()
    {
        Transform pos = spawnPoints[Random.Range(0, spawnPoints.Length)]; 
        GameObject powerUp = Instantiate(powerUps[Random.Range(0, powerUps.Length)], pos.position , Quaternion.identity);
        Destroy(powerUp, destroyTime); 
    }
}
