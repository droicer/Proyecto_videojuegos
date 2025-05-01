using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Moverse : MonoBehaviour
{
    public float velocidad = 2f; // Velocidad del zombi
    private Rigidbody2D rb;
    private bool moviendoDerecha = true;
    public Collider2D colliderZombi; // Collider del zombi

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        colliderZombi = GetComponent<Collider2D>();

        // Ignorar colisiones con hongos
        Collider2D[] hongos = GameObject.FindGameObjectsWithTag("Hongos")
            .Select(h => h.GetComponent<Collider2D>())
            .Where(c => c != null)
            .ToArray();

        foreach (var hongo in hongos)
        {
            Physics2D.IgnoreCollision(colliderZombi, hongo);
        }

        // Ignorar colisiones con otros zombis
        GameObject[] todosZombis = GameObject.FindGameObjectsWithTag("Zombi");
        foreach (GameObject otroZombi in todosZombis)
        {
            if (otroZombi != this.gameObject)
            {
                Collider2D colOtro = otroZombi.GetComponent<Collider2D>();
                if (colOtro != null)
                {
                    Physics2D.IgnoreCollision(colliderZombi, colOtro);
                }
            }
        }
    }

    void FixedUpdate()
    {
        float direccion = moviendoDerecha ? 1f : -1f;
        rb.velocity = new Vector2(direccion * velocidad, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared"))
        {
            Girar();
        }
    }

    void Girar()
    {
        moviendoDerecha = !moviendoDerecha;

        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}
