using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Rate per second.
    [SerializeField] float spawnRate = 2;
    [SerializeField] GameObject enemyToSpawn;

    private float xMin;
    private float xMax;
    private float ySpawn;

    // Start is called before the first frame update
    void Start()
    {
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x;
        ySpawn = Camera.main.ViewportToWorldPoint(new Vector3(0, 1.25f, 0)).y;
        InvokeRepeating("SpawnEnemy", 0, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(xMin, xMax);
        Instantiate(enemyToSpawn, new Vector3(randomX, ySpawn, 0), Quaternion.identity);
    }
}
