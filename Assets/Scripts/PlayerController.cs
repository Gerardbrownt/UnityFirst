using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.5f;
    public float distanciaMuerte = 1.5f;
    public Animator animator;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float fireTimer;
    private bool facingRight = true;
    private bool estaMuerto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (estaMuerto) return;

        float move = 0f;
        if (Input.GetKey(KeyCode.D)) move = 1f;
        else if (Input.GetKey(KeyCode.A)) move = -1f;

        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(move));

        if ((move > 0 && !facingRight) || (move < 0 && facingRight))
        {
            Flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        animator.SetBool("IsJumping", !isGrounded);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        fireTimer -= Time.deltaTime;
        if ((Input.GetKey(KeyCode.K) || Input.GetMouseButton(0)) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }

        // Muerte por caída
        if (transform.position.y < -10f)
        {
            Muerte(true);
        }

        // Muerte por proximidad a enemigos
        GameObject[] posibles = GameObject.FindGameObjectsWithTag("Destruible");
        foreach (GameObject obj in posibles)
        {
            if (obj.GetComponent<Enemy>() == null) continue; // Ignora monedas u otros

            float distancia = Vector2.Distance(transform.position, obj.transform.position);
            if (distancia < distanciaMuerte)
            {
                Muerte(false);
                break;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;

        Vector3 posDisparo = firePoint.localPosition;
        posDisparo.x *= -1;
        firePoint.localPosition = posDisparo;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(facingRight ? Vector2.right : Vector2.left);
    }

    void Muerte(bool esCaida)
    {
        if (estaMuerto) return;
        estaMuerto = true;

        animator.SetTrigger("Muere");

        // Posición para que se vea la animación (más arriba si es por caída)
        Vector3 nuevaPos = transform.position;
        if (esCaida)
            nuevaPos += Vector3.up * 10f;

        transform.position = nuevaPos;

        rb.linearVelocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;

        Invoke(nameof(ReiniciarMundoCompleto), 1f);
    }

    void ReiniciarMundoCompleto()
    {
        GameManager.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanciaMuerte);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Destruible") && collision.collider.GetComponent<Enemy>() != null)
        {
            Vector2 direccion = collision.contacts[0].normal;

            if (direccion.y < -0.5f)
            {
                Destroy(collision.collider.gameObject); // Lo pisa
            }
            else
            {
                Muerte(false); // Lo toca de lado
            }
        }
    }
}
