using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Rate per second.
    [SerializeField] float spawnRate = 2;
    [SerializeField] GameObject blueEnemy;
    [SerializeField] GameObject greenEnemy;

    private float xMinBlue, xMaxBlue, ySpawnBlue;
    private float yMinGreen, yMaxGreen;
    private float xSpawnLeftGreen, xSpawnRightGreen;

    void Start()
    {
        xMinBlue = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x;
        xMaxBlue = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x;
        ySpawnBlue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.25f, 0)).y;
        xSpawnLeftGreen = Camera.main.ViewportToWorldPoint(new Vector3(-0.25f, 0, 0)).x;
        xSpawnRightGreen = Camera.main.ViewportToWorldPoint(new Vector3(1.25f, 0, 0)).x;
        yMinGreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.6f, 0)).y;
        yMaxGreen = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.9f, 0)).y;
        InvokeRepeating("SpawnEnemyBlue", 0, spawnRate);
        InvokeRepeating("SpawnEnemyGreen", 0, spawnRate);
    }

    void SpawnEnemyBlue()
    {
        float randomX = UnityEngine.Random.Range(xMinBlue, xMaxBlue);
        Instantiate(blueEnemy, new Vector3(randomX, ySpawnBlue, 0), Quaternion.identity);
    }

    void SpawnEnemyGreen()
    {
        bool leftSpawn = Convert.ToBoolean(UnityEngine.Random.Range(0, 1));
        float xSpawnCoord = leftSpawn ? xSpawnLeftGreen : xSpawnRightGreen;
        float ySpawnCoord = UnityEngine.Random.Range(yMinGreen, yMaxGreen);
        var eg = Instantiate(greenEnemy, new Vector3(xSpawnCoord, ySpawnCoord, 0), Quaternion.identity);
        eg.GetComponent<EnemyGreen>().leftSpawn = leftSpawn;
    }
}
