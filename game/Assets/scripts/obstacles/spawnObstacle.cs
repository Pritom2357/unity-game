using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnObstacle : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("SpawnObstacle", 0f, spawnInterval);
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPosition = player.position + (Vector3)(Random.insideUnitCircle.normalized * spawnRadius);
        Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
    }
}
