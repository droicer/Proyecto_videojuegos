using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saltare : MonoBehaviour
{
    public float jumpForce = 10f; // Fuerza del salto
    private Rigidbody2D rb; // Referencia al Rigidbody2D
    private bool isGrounded; // Para verificar si está en el suelo

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D
    }

    // Update is called once per frame
    void Update()
    {
        // Verificar si se presiona espacio y si está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();

        }
    }

    void Jump()
    {
        // Aplicar fuerza vertical para el salto
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionó con: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pared"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pared"))
        {
            isGrounded = false;
        }
    }

}