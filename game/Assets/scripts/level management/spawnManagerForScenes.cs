using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneEnemies
    {
        public string sceneName; // Optional name for reference
        public GameObject[] enemies; // Array of enemies for this scene
    }

    public List<SceneEnemies> scenes = new List<SceneEnemies>();
    public float spawnInterval = 2f;
    public float minSpawnRadius = 5f;
    public float maxSpawnRadius = 10f;
    public float obstacleLifetime = 5f;
    public int spawnsBeforeIncrease = 5;
    public float lifetimeIncreaseAmount = 2f;

    private Transform playerTransform;
    private playerLevelController playerLevel; // Reference to player's level controller
    private int currentSceneIndex = 0;
    private int spawnCount = 0;
    private List<GameObject> spawnedEnemies = new List<GameObject>(); // List to keep track of spawned enemies
    public int maxEnemyCount = 10;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerLevel = playerTransform.GetComponent<playerLevelController>();

        if (playerLevel == null)
        {
            Debug.LogError("PlayerLevelController not found on the player!");
            return;
        }

        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            int enemyCount = GetActiveEnemyCount();
            if(enemyCount < maxEnemyCount){ 
                SpawnEnemyForCurrentLevel();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemyForCurrentLevel()
    {
        if (currentSceneIndex >= scenes.Count || playerLevel == null) return;

        int level = playerLevel.GetPlayerLevel();
        SceneEnemies currentSceneEnemies = scenes[currentSceneIndex];

        // Determine which enemy to spawn based on the player's level
        GameObject enemyToSpawn = GetEnemyBasedOnLevel(level, currentSceneEnemies);

        // Ensure there is an enemy to spawn
        if (enemyToSpawn != null)
        {
            Vector3 spawnPosition = GetRandomPositionAroundPlayer();
            GameObject spawnedEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(spawnedEnemy); // Add the spawned enemy to the list
            Destroy(spawnedEnemy, obstacleLifetime);
        }

        // Track spawns and adjust lifetime after a certain number of spawns
        spawnCount++;
        if (spawnCount >= spawnsBeforeIncrease)
        {
            obstacleLifetime += lifetimeIncreaseAmount;
            spawnCount = 0;
        }

        // Check if it's time to move to the next scene
        CheckForSceneChange(level);
    }

    private GameObject GetEnemyBasedOnLevel(int level, SceneEnemies sceneEnemies)
    {
        int levelWithinScene = (level - 1) % 5;

        if (levelWithinScene == 0 || levelWithinScene==1)
            return sceneEnemies.enemies[0]; // Easy enemy for levels like 1, 6, 11...
        else if (levelWithinScene == 2 || levelWithinScene == 3)
            return sceneEnemies.enemies[1]; // Medium enemy for levels like 2, 3, 7, 8, 12, 13...
        else
            return sceneEnemies.enemies[2]; // Hard enemy for levels like 4, 5, 9, 10, 14, 15...
    }

    private Vector3 GetRandomPositionAroundPlayer()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomDirection.x, randomDirection.y, 0) * randomDistance;
        return spawnPosition;
    }

    private void CheckForSceneChange(int playerLevel)
    {
        if (playerLevel > 0 && playerLevel % 5 == 1)
        {
            // Destroy all existing enemies before changing the scene
            // ClearAllEnemies();
            if(playerLevel>20){
                currentSceneIndex=4;
            }else if(playerLevel>15){
                currentSceneIndex=3;
            }else if(playerLevel>10){
                currentSceneIndex=2;
            }else if(playerLevel>5){
                currentSceneIndex=1;
            }else{
                currentSceneIndex=0;
            }

            // Move to the next scene
            if (currentSceneIndex >= scenes.Count)
            {
                Debug.Log("All scenes completed!");
                currentSceneIndex = scenes.Count - 1; // Stay on the last scene
            }
        }
    }

    private int GetActiveEnemyCount()
    {
        // Clean up null references in the list (enemies that have been destroyed)
        spawnedEnemies.RemoveAll(enemy => enemy == null);
        return spawnedEnemies.Count;
    }


    private void ClearAllEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        spawnedEnemies.Clear(); // Clear the list after destroying all enemies
    }
}
