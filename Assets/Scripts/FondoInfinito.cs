using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    public GameObject fondoPrefab;
    public Transform jugador;
    public float anchoFondo = 24f;
    private float ultimaXFondo = 0f;

    void Start()
    {
        ultimaXFondo = fondoPrefab.transform.position.x;
    }

    void Update()
    {
        if (jugador.position.x + 10f > ultimaXFondo)
        {
            Vector3 nuevaPos = new Vector3(ultimaXFondo + anchoFondo, fondoPrefab.transform.position.y, fondoPrefab.transform.position.z);
            Instantiate(fondoPrefab, nuevaPos, Quaternion.identity);
            ultimaXFondo += anchoFondo;
        }
    }
}
