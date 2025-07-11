using UnityEngine;
using UnityEngine.Rendering;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] coins;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float coinSpawnInterval;
    private float tempInterval;
    private PlayerMove player;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
        tempInterval = coinSpawnInterval; 
    }
    private void Update()
    {
        if (player.receiveInput)
        {
            coinSpawnInterval -= Time.deltaTime;
            if (coinSpawnInterval <= 0)
            {
                spawn();
                coinSpawnInterval = tempInterval;
            }
        }
    }
    private void spawn()
    {
       
         int index1 = Random.Range(0, spawnPoints.Length);
        int index2;
        do
        {
            index2 = Random.Range(0, spawnPoints.Length);
        } while (index2 == index1);
        Vector3 Pos1 = new Vector3(spawnPoints[index1].position.x, spawnPoints[index1].position.y, 1);
        Vector3 Pos2 = new Vector3(spawnPoints[index2].position.x, spawnPoints[index2].position.y, 1);
        GameObject coin1 =  Instantiate(coins[Random.Range(0, coins.Length)],Pos1, Quaternion.identity);
       GameObject coin2 =  Instantiate(coins[Random.Range(0, coins.Length)], Pos2, Quaternion.identity);
        Destroy(coin1, 60f);
        Destroy(coin2, 60f); 
    }
 
}
