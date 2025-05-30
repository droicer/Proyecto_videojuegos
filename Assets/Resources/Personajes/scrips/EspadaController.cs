using UnityEngine;

public class EspadaController : MonoBehaviour
{
    private string direccion = "Derecha";

    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        if (direccion == "Derecha")
        {
            rb.velocity = new Vector2(15, rb.velocity.y);
            sr.flipY = false;
        }
        else if (direccion == "Izquierda")
        {
            rb.velocity = new Vector2(-15, rb.velocity.y);
            sr.flipY = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pared"))
        {

            Destroy(this.gameObject); // La espada desaparece igual

        }

        if (collision.CompareTag("Zombi"))
        {
            Vidas vidas = collision.GetComponent<Vidas>();
            if (vidas != null)
            {
                vidas.RecibirDaño(1); // Le quita 1 vida
            }

            Destroy(this.gameObject); // La espada desaparece igual
        }
    }

    public void SetDirection(string direction)
    {
        this.direccion = direction;
    }
}
