using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public Transform jugador;
    public float distanciaLimite = 20f;

    void Update()
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag("Destruible");

        foreach (GameObject obj in objetos)
        {
            if (jugador != null && obj != null)
            {
                if (obj.transform.position.x < jugador.position.x - distanciaLimite)
                {
                    Destroy(obj);
                }
            }
        }
    }
}
