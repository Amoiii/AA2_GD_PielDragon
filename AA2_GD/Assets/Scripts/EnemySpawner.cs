using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool isActive;
    
    public GameObject enemyPrefab;
    public float spawnRate = 3f; 
    private float nextSpawnTime;

    void Update()
    {
        if (!isActive) return;
        
        if (Time.time >= nextSpawnTime)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-6f, 6f));
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            nextSpawnTime = Time.time + spawnRate;
        }
    }
}