using UnityEngine;

public class Animaciones : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        float movimiento = Input.GetAxisRaw("Horizontal");

        // Si está en el suelo y no se está moviendo verticalmente
        if (isGrounded && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            if (movimiento != 0)
            {
                animator.SetInteger("Estado", 1); // Caminando
            }
            else
            {
                animator.SetInteger("Estado", 0); // Quieto
            }
        }
        else if (rb.velocity.y > 0.1f)
        {
            animator.SetInteger("Estado", 2); // Saltando
        }
        else if (rb.velocity.y < -0.1f)
        {
            animator.SetInteger("Estado", 3); // Cayendo
        }
    }

    // Dibuja el círculo del groundCheck en el editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
        }
    }
}
