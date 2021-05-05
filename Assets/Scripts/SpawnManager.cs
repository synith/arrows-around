using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject pickUp;

    public float spawnRange = 10;
    public float pickUpSpawnRange = 8;
    public float enemyDistance = 12;
    public float pickUpDistance = 8;

    public float enemySpawnRate = 3;
    public float pickUpSpawnRate = 5;

    // Start is called before the first frame update
    void Start()
    {        
        InvokeRepeating("SpawnEnemy", 3f, enemySpawnRate);
        InvokeRepeating("SpawnPickUp", 5f, pickUpSpawnRate);
    }
    void SpawnEnemy()
    {
        float spawnRangeRandom = Random.Range(-spawnRange, spawnRange);
        
        Vector3 spawnPosTop = new Vector3(spawnRangeRandom, 0.5f, enemyDistance);
        Vector3 spawnPosBot = new Vector3(spawnRangeRandom, 0.5f, -enemyDistance);
        Vector3 spawnPosRight = new Vector3(enemyDistance, 0.5f, spawnRangeRandom);
        Vector3 spawnPosLeft = new Vector3(-enemyDistance, 0.5f, spawnRangeRandom);

        Vector3[] spawnPosition ={spawnPosTop,spawnPosBot,spawnPosRight,spawnPosLeft};
        int randomIndex = Random.Range(0, 4);
        Instantiate(enemy, spawnPosition[randomIndex], enemy.transform.rotation);        
    }
    void SpawnPickUp()
    {
        float spawnRangeRandom = Random.Range(-pickUpSpawnRange, pickUpSpawnRange);

        Vector3 spawnPosTop = new Vector3(spawnRangeRandom, 0.5f, pickUpDistance);
        Vector3 spawnPosBot = new Vector3(spawnRangeRandom, 0.5f, -pickUpDistance);
        Vector3 spawnPosRight = new Vector3(pickUpDistance, 0.5f, spawnRangeRandom);
        Vector3 spawnPosLeft = new Vector3(-pickUpDistance, 0.5f, spawnRangeRandom);

        Vector3[] spawnPosition = { spawnPosTop, spawnPosBot, spawnPosRight, spawnPosLeft };
        int randomIndex = Random.Range(0, 4);
        Instantiate(pickUp, spawnPosition[randomIndex], pickUp.transform.rotation);
    }
}
