using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 3f; // Sale un enemigo cada 3 segundos
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // Crea un enemigo en una posición aleatoria en los bordes de la cámara
            Vector2 randomPosition = new Vector2(Random.Range(-10f, 10f), Random.Range(-6f, 6f));
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            nextSpawnTime = Time.time + spawnRate;
        }
    }
}