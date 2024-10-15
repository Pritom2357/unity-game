using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles; 
    public float spawnInterval = 2f; 
    public float spawnRangeX = 5f; 
    public float spawnRangeY = 5f; 
    public float minDistanceFromPlayer = 2f; 
    public float obstacleLifetime = 5f; 
    public int spawnsBeforeIncrease = 5; 
    public float lifetimeIncreaseAmount = 2f; 

    private Transform playerTransform;
    private int spawnCount = 0; 

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            SpawnRandomObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnRandomObstacle()
    {
        Vector2 spawnPosition;
        do
        {
            spawnPosition = new Vector2(
                Random.Range(-spawnRangeX, spawnRangeX),
                Random.Range(-spawnRangeY, spawnRangeY)
            );
        } while (Vector2.Distance(spawnPosition, playerTransform.position) < minDistanceFromPlayer);

        int randomIndex = Random.Range(0, obstacles.Length);
        GameObject obstacle = Instantiate(obstacles[randomIndex], spawnPosition, Quaternion.identity);

        Destroy(obstacle, obstacleLifetime);

        spawnCount++;

        if (spawnCount >= spawnsBeforeIncrease)
        {
            obstacleLifetime += lifetimeIncreaseAmount;
            spawnCount = 0;
        }
    }
}
