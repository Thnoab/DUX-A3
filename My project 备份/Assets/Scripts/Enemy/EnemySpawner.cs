using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 1f;  // 生成间隔
    private float spawnTimer;
    private int spawnedCount = 0;
    public int maxEnemies = 9;

    void Start()
    {
        spawnTimer = 0;
    }

    void Update()
    {
        // 如果已经生成足够数量就不再生成
        if (spawnedCount >= maxEnemies) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnTime)
        {
            spawnTimer = 0;
            SpawnEnemy();
            spawnedCount++;
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}