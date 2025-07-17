using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;
    public float cooldownIfEnemyPresent = 10f;

    private float cooldownTimer = 0f;
    private bool initialSpawnDone = false;

    void Start()
    {
        if (spawnPoints.Length > 0)
        {
            // Hacer spawn inicial en todos los puntos
            foreach (Transform point in spawnPoints)
            {
                Instantiate(enemyPrefab, point.position, Quaternion.identity);
            }

            initialSpawnDone = true;

            // Iniciar spawner en bucle
            InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
        }
        else
        {
            Debug.LogError("No spawn points assigned!");
        }
    }

    void SpawnEnemy()
    {
        // Esperar si ya hay enemigos
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            cooldownTimer += spawnInterval;

            if (cooldownTimer < cooldownIfEnemyPresent)
            {
                Debug.Log("Cooldown active. Not spawning enemy.");
                return;
            }
            else
            {
                cooldownTimer = 0f;
            }
        }

        // Intentar spawnear en un punto aleatorio
        int attempts = 0;
        int maxAttempts = spawnPoints.Length;

        while (attempts < maxAttempts)
        {
            int index = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPosition = spawnPoints[index].position;

            Collider2D hit = Physics2D.OverlapCircle(spawnPosition, 0.5f, LayerMask.GetMask("Enemy"));

            if (hit == null)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                return;
            }

            attempts++;
        }

        Debug.LogWarning("All spawn points are blocked.");
    }

    void OnDrawGizmosSelected()
    {
        if (spawnPoints != null)
        {
            Gizmos.color = Color.red;
            foreach (Transform t in spawnPoints)
            {
                Gizmos.DrawWireSphere(t.position, 0.5f);
            }
        }
    }
}
