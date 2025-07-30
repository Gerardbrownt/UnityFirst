using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Transform[] puntosDeSpawn;

    void Start()
    {
        foreach (Transform punto in puntosDeSpawn)
        {
            if (enemigoPrefab != null && punto != null)
            {
                GameObject enemigo = Instantiate(enemigoPrefab, punto.position, Quaternion.identity);

                // Asegura que tenga el tag correcto para el GarbageCollector
                enemigo.tag = "Destruible";

                // Evita que el enemigo quede como hijo del bloque (por si el bloque luego se destruye)
                enemigo.transform.parent = null;
            }
        }
    }
}
