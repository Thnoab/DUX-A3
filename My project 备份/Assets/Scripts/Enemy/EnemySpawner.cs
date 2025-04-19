using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTime = 1f;  // ���ɼ��
    private float spawnTimer;
    private int spawnedCount = 0;
    public int maxEnemies = 9;

    void Start()
    {
        spawnTimer = 0;
    }

    void Update()
    {
        // ����Ѿ������㹻�����Ͳ�������
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