using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Rate per second.
    [SerializeField] float spawnRateBlue = 2;
    [SerializeField] float spawnRateGreen = 3;

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
        
        // Prepare spawn mechanics. The methods will reinvoke themselves based on the point spawn multiplier. 
        Invoke(nameof(SpawnEnemyBlue), 1);
        Invoke(nameof(SpawnEnemyGreen), 2);
        // InvokeRepeating("SpawnEnemy", 0, spawnRate); 
    }

    // GameManager tracks a multiplier that makes enemies spawn faster the more points you have. 
    // Losing points drops you to the previous tier of speed. 
    // Current speed is set at spawnRate * (0.9^(score / 100))
    void SpawnEnemyBlue()
    {
        float randomX = UnityEngine.Random.Range(xMinBlue, xMaxBlue);
        Instantiate(blueEnemy, new Vector3(randomX, ySpawnBlue, 0), Quaternion.identity);

        Invoke(nameof(SpawnEnemyBlue), GameManager.instance.GetSpawnBasedScoreMult() * spawnRateBlue);
    }

    void SpawnEnemyGreen()
    { // Green enemy spawns on a random side of the screen. 
        bool leftSpawn = UnityEngine.Random.Range(0, 2) == 1; 
        float xSpawnCoord = leftSpawn ? xSpawnLeftGreen : xSpawnRightGreen;
        float ySpawnCoord = UnityEngine.Random.Range(yMinGreen, yMaxGreen);
        var eg = Instantiate(greenEnemy, new Vector3(xSpawnCoord, ySpawnCoord, 0), Quaternion.identity);
        eg.GetComponent<EnemyGreen>().leftSpawn = leftSpawn;

        Invoke(nameof(SpawnEnemyGreen), GameManager.instance.GetSpawnBasedScoreMult() * spawnRateGreen);
    }
}
