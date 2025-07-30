using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Muere");

        GetComponent<Collider2D>().enabled = false; // ← OPCIONAL
        GameManager.Instance.AddScore(2);

        Destroy(gameObject, 0.5f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}
