using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bola_de_fuego_controller : MonoBehaviour
{
    public float velocidad = 15f; // ? Velocidad pública editable

    private string direccion = "Derecha";

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        Destroy(this.gameObject, 5f);
        ingnorarCollisionZombi();


    }

    void ingnorarCollisionZombi()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // Ignorar colisión con todos los zombis al iniciar
        GameObject[] zombis = GameObject.FindGameObjectsWithTag("Zombi");
        Collider2D myCol = GetComponent<Collider2D>();

        foreach (GameObject z in zombis)
        {
            Collider2D zCol = z.GetComponent<Collider2D>();
            if (myCol != null && zCol != null)
            {
                Physics2D.IgnoreCollision(myCol, zCol);
            }
        }

        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        if (direccion == "Derecha")
        {
            rb.velocity = new Vector2(velocidad, rb.velocity.y);
            sr.flipY = false;
        }
        else if (direccion == "Izquierda")
        {
            rb.velocity = new Vector2(-velocidad, rb.velocity.y);
            sr.flipY = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }


    public void SetDirection(string direction)
    {
        this.direccion = direction;
    }
}
