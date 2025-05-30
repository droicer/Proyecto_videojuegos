using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Saltare : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody2D rb;

    [Header("Ground Check")]
    public Transform groundCheck;            // Objeto para verificar el suelo
    public float checkRadius = 0.2f;         // Radio de detección
    public LayerMask groundLayer;            // Capa de suelo

    private bool isGrounded;

    [Header("Audio")]
    public AudioClip jumpSound;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Detectar si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Opcional dentro de Jump()
        rb.velocity = new Vector2(rb.velocity.x, 0); // Reset vertical
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);


        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    // Visual para ayudarte a ajustar el Ground Check
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}
