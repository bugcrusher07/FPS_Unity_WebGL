
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemyPrefab;
//     public float spawnInterval = 5f;
//     private float timer = 0f;

//     void Start()
//     {

//     }

//     void Update()
//     {
//         timer += Time.deltaTime;

//         if (timer >= spawnInterval)
//         {
//             Instantiate(enemyPrefab);
//             timer = 0f;
//         }
//     }
// }
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public float spawnInterval = 5f;
    public bool isSpawning = true;

    [Header("Speed Settings")]
    public float baseSpeed = 3.5f;
    public float speedIncrement = 0.5f; // How much faster each enemy gets

    private float timer = 0f;
    private int spawnCount = 0;

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

            if (enemy)
            {
                NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    float newSpeed = baseSpeed + (spawnCount * speedIncrement);
                    agent.speed = newSpeed;

                    Debug.Log($"Enemy {spawnCount + 1} spawned with speed: {newSpeed}");
                }
            }

            spawnCount++;
        }
    }

    public void StartSpawning() { isSpawning = true; }
    public void StopSpawning() { isSpawning = false; }
}