using UnityEngine;
using System.Collections;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laserPrefab;  
    public float initialSpawnDelay = 2f; 
    public float spawnInterval = 2f;  
    public int lasersToSpawn = 2;  
    public float increaseInterval = 10f;  
    public int increaseAmount = 2; 
    public float increaseDelay = 20f;  

    public float minX = -3f;  
    public float maxX = 13f;  
    public float minZ = -13f; 
    public float maxZ = 3f;   

    private float spawnTimer;
    private float increaseTimer;

    void Start()
    {
        spawnTimer = initialSpawnDelay;
        increaseTimer = increaseDelay;
        StartCoroutine(SpawnLaser());
    }

    IEnumerator SpawnLaser()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            for (int i = 0; i < lasersToSpawn; i++)
            {
                float randomX = Mathf.Round(Random.Range(minX, maxX) / 3f) * 3f;
                float randomZ = Mathf.Round(Random.Range(minZ, maxZ) / 3f) * 3f;
                Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ);

                LaserIndicator instance = ObjectPooler.DequeueObject<LaserIndicator>("Lasers");
                instance.transform.position = spawnPosition;
                instance.transform.rotation = Quaternion.identity;
            }

            yield return new WaitForSeconds(spawnInterval);

            spawnTimer += spawnInterval;

            if (spawnTimer >= increaseTimer)
            {
                lasersToSpawn += increaseAmount;
                increaseTimer += increaseDelay;
            }
        }
    }
}