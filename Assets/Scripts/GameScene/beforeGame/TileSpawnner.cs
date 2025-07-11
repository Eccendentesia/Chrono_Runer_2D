using UnityEngine;

public class TileSpawnner : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private int maxCount;

   

    void Start()
    {
        spawnTile();
        //spawnPlayer();
    }

    public void spawnTile()
    {
        GameObject tile;
       
        if(maxCount <= 1)
        {
            tile = Instantiate(tilePrefabs[0], spawnPos, Quaternion.identity);
            spawnPos = tile.transform.GetChild(0).transform.position;
            maxCount++;
        }
        else
        {
            tile = Instantiate(tilePrefabs[1], spawnPos, Quaternion.identity);
            spawnPos = tile.transform.GetChild(0).transform.position;
            
        }
    
    }

}
