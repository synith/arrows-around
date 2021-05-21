using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject pickUp;

    private GameManager gameManager;

    [SerializeField]
    private float
        spawnRange = 10,
        enemyDistance = 12,
        enemySpawnRate = 10;

    [SerializeField]
    private float
        pickUpSpawnRange = 8,
        pickUpSpawnRate = 30,
        pickUpDistance = 9;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    public void SpawnManagerStart()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, enemySpawnRate);
        InvokeRepeating(nameof(SpawnPickUp), pickUpSpawnRate, pickUpSpawnRate);
    }
    void SpawnEnemy()
    {
        if (!gameManager.gameOver)
        {
            float spawnRangeRandom = Random.Range(-spawnRange, spawnRange);

            Vector3 spawnPosTop = new Vector3(spawnRangeRandom, 0.5f, enemyDistance);
            Vector3 spawnPosBot = new Vector3(spawnRangeRandom, 0.5f, -enemyDistance);
            Vector3 spawnPosRight = new Vector3(enemyDistance, 0.5f, spawnRangeRandom);
            Vector3 spawnPosLeft = new Vector3(-enemyDistance, 0.5f, spawnRangeRandom);

            Vector3[] spawnPosition = { spawnPosTop, spawnPosBot, spawnPosRight, spawnPosLeft };
            int randomIndex = Random.Range(0, 4);
            Instantiate(enemy, spawnPosition[randomIndex], enemy.transform.rotation);
        }

    }
    void SpawnPickUp()
    {
        if (!gameManager.gameOver)
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
}
