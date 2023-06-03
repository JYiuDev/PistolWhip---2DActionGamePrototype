using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public Transform[] spawnLocations;
    public float spawnRadius = 2f;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 5f;
    public int maxEnemies = 7;
    public int maxSpawns = 0;

    private int currentEnemyCount;

    private void Start()
    {
        // StartCoroutine(SpawnEnemies());
        GameObject.FindGameObjectWithTag("EndObject").SetActive(false);
    }

    private void Update()
    {
        currentEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (currentEnemyCount == 1 && maxSpawns < 20)
        {
            StartCoroutine(SpawnEnemies());
        }

        if (maxSpawns >= 20)
        {
            StopCoroutine(SpawnEnemies());
        }

        if (maxSpawns >= 20 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            GameObject.FindGameObjectWithTag("EndObject").SetActive(true);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Wait until there is only 1 enemy remaining
            while (currentEnemyCount > 1)
            {
                yield return null;
            }

            // Spawn enemies until there are 10
            while (currentEnemyCount < maxEnemies && maxSpawns < 20)
            {
                // Randomly select the enemy prefab
                GameObject enemyPrefab = Random.Range(0, 2) == 0 ? enemyPrefab1 : enemyPrefab2;

                // Randomly select a spawn location
                Transform spawnLocation = spawnLocations[Random.Range(0, spawnLocations.Length)];

                // Offset the spawn position within the specified radius
                //Vector2 spawnPosition = spawnLocation.position + Random.insideUnitCircle * spawnRadius;
                // Offset the spawn position within the specified radius
                Vector2 spawnOffset = Random.insideUnitCircle * spawnRadius;
                Vector2 spawnPosition = (Vector2)spawnLocation.position + spawnOffset;

                // Instantiate the enemy prefab at the spawn position
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                // Increase the enemy count
                //currentEnemyCount++;
                maxSpawns++;

                yield return new WaitForSeconds(Random.Range(minSpawnRate, maxSpawnRate));
            }

            // Wait until there is only 1 enemy remaining
            while (currentEnemyCount > 1)
            {
                yield return null;
            }
        }
    }
}
