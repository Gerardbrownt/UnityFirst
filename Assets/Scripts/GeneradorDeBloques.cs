using UnityEngine;

public class GeneradorDeBloques : MonoBehaviour
{
    public GameObject bloquePrefab;
    public GameObject monedaPrefab;
    public Transform jugador;
    public float distanciaParaGenerar = 30f;
    public float anchoBloque = 24f;

    private float siguienteBloqueX;
    private float alturaBloqueY;

    void Start()
    {
        siguienteBloqueX = bloquePrefab.transform.position.x + anchoBloque;
        alturaBloqueY = bloquePrefab.transform.position.y;
    }

    void Update()
    {
        while (siguienteBloqueX < jugador.position.x + distanciaParaGenerar)
        {
            GenerarBloqueEn(siguienteBloqueX);
            siguienteBloqueX += anchoBloque;
        }
    }

    void GenerarBloqueEn(float x)
    {
        Vector3 nuevaPos = new Vector3(x, alturaBloqueY, 0f);
        GameObject nuevoBloque = Instantiate(bloquePrefab, nuevaPos, Quaternion.identity);

        Transform suelos = nuevoBloque.transform.Find("Suelos");
        if (suelos != null && suelos.childCount > 0)
        {
            Transform plataforma = suelos.GetChild(Random.Range(0, suelos.childCount));
            BoxCollider2D col = plataforma.GetComponent<BoxCollider2D>();

            if (col != null && Random.value < 0.7f)
            {
                float posY = col.bounds.max.y + 1.2f;
                Instantiate(monedaPrefab, new Vector3(col.bounds.center.x, posY, 0f), Quaternion.identity);
            }
        }
    }
}
