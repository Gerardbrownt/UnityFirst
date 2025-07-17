using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void TakeDamage()
    {
        // Aquí puedes añadir efectos antes de morir
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}
