using UnityEngine;

public class CameraSeguirJugador : MonoBehaviour
{
    public Transform jugador;
    public float offsetExtraX = -2f; // Ajuste para ver más hacia adelante o atrás

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - jugador.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(jugador.position.x + offset.x + offsetExtraX, offset.y, transform.position.z);
    }
}
