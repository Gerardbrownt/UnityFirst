using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 2f;
    public float xMin = -5f, xMax = 5f, yMin = 0f, yMax = 3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnCoin), 1f, spawnInterval);
    }

    void SpawnCoin()
    {
        Vector2 spawnPos = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
        Instantiate(coinPrefab, spawnPos, Quaternion.identity);
    }
}
