using UnityEngine;

public class GeneradorDeBloques : MonoBehaviour
{
    public GameObject bloquePrefab;
    public GameObject enemigoPrefab;
    public Transform jugador;
    public float distanciaParaGenerar = 10f;
    public float anchoBloque = 24f;

    private float ultimaPosicionX;
    private float alturaBloqueY;
    private int cantidadBloquesInstanciados = 1;

    void Start()
    {
        ultimaPosicionX = bloquePrefab.transform.position.x;
        alturaBloqueY = bloquePrefab.transform.position.y;
    }

    void Update()
    {
        if (jugador.position.x + distanciaParaGenerar > ultimaPosicionX + (anchoBloque * (cantidadBloquesInstanciados - 1)))
        {
            Vector3 nuevaPos = new Vector3(ultimaPosicionX + (anchoBloque * cantidadBloquesInstanciados), alturaBloqueY, 0f);
            GameObject nuevoBloque = Instantiate(bloquePrefab, nuevaPos, Quaternion.identity);

            // Buscar el objeto "Suelos"
            Transform suelos = nuevoBloque.transform.Find("Suelos");

            if (suelos != null && suelos.childCount > 0)
            {
                Transform plataforma = suelos.GetChild(Random.Range(0, suelos.childCount));
                    BoxCollider2D col = plataforma.GetComponent<BoxCollider2D>();

                    if (col != null)
                    {
                        float altura = col.bounds.size.y;
                        float posY = col.bounds.max.y + 0.1f; // un pequeño margen arriba
                        Vector3 posEnemigo = new Vector3(col.bounds.center.x, posY, 0f);
                        Instantiate(enemigoPrefab, posEnemigo, Quaternion.identity);
                    }
                    else
                    {
                        Debug.LogWarning("La plataforma no tiene BoxCollider2D");
                    }


            }
            else
            {
                Debug.LogWarning("No se encontró el objeto 'Suelos' o no tiene hijos.");
            }

            cantidadBloquesInstanciados++;
        }
    }
}
