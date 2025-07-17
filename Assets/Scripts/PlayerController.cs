using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 20f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float fireTimer;
    private bool facingRight = true;

    private Vector3 startPosition; // Para respawn

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startPosition = transform.position;
    }

    void Update()
    {
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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Disparo solo al presionar click izquierdo o tecla
        fireTimer -= Time.deltaTime;
        if ((Input.GetKey(KeyCode.K) || Input.GetMouseButton(0)) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }

        // Respawn si cae
        if (transform.position.y < -10f)
        {
            Respawn();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        Vector3 firePointPos = firePoint.localPosition;
        firePointPos.x *= -1;
        firePoint.localPosition = firePointPos;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(facingRight ? Vector2.right : Vector2.left);
    }

    void Respawn()
    {
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
    }
}
