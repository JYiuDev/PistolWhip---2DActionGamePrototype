using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public Transform[] spawnLocations;
    public float spawnRadius = 0.3f;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 5f;
    public int maxEnemies = 7;
    public int maxSpawns = 0;

    private int currentEnemyCount;
    private bool isSpawning;
    private float spawnTimer;

    private void Start()
    {
        isSpawning = false;
        spawnTimer = 0f;
        GameObject.FindGameObjectWithTag("EndObject").SetActive(false);
    }

    public GameObject exit;

    private void Update()
    {
        currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemyCount <= 1 && maxSpawns < 20 && !isSpawning)
        {
            StartSpawning();
        }

        if (maxSpawns >= 20)
        {
            StopSpawning();
        }

        if (maxSpawns >= 20 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            exit.SetActive(true);
        }

        if (isSpawning)
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0f)
            {
                SpawnEnemy();
                spawnTimer = Random.Range(minSpawnRate, maxSpawnRate);
            }
        }
    }

    private void StartSpawning()
    {
        isSpawning = true;
        spawnTimer = 0f;
    }

    private void StopSpawning()
    {
        isSpawning = false;
        spawnTimer = 0f;
    }

    private void SpawnEnemy()
    {
        if (currentEnemyCount < maxEnemies && maxSpawns < 20)
        {
            // Randomly select the enemy prefab
            GameObject enemyPrefab = Random.Range(0, 2) == 0 ? enemyPrefab1 : enemyPrefab2;

            // Randomly select a spawn location
            Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];

            // Offset the spawn position within the specified radius
            Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius;
            Vector2 spawnPosition = (Vector2)spawnLocation.position + spawnOffset;

            // Instantiate the enemy prefab at the spawn position
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Increase the enemy count
            maxSpawns++;
        }
    }
}
