using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Jefe_final : MonoBehaviour
{
    public int vida = 14;
    private int vecesQueHaCrecido = 0;

    public GameObject zombiPrefab;
    public Transform puntoSpawn;
    private bool invocandoZombis = false;

    public GameObject bolaDeFuegoPrefab;
    public Transform puntoDisparo;
    public float offsetYBolaFuego = -0.3f; // ← Altura ajustable del disparo

    private bool lanzandoBolas = false;
    private string direccion = "Derecha"; // Dirección actual

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("¡Presionaste espacio!");
        }
    }

    public void RecibirDaño_espada(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("Vida actual: " + vida);

        int bloquesDeDosPerdidos = (26 - vida) / 2;

        if (bloquesDeDosPerdidos > vecesQueHaCrecido)
        {
            Crecer();
            vecesQueHaCrecido = bloquesDeDosPerdidos;
        }

        if (vida <= 16 && !invocandoZombis)
        {
            invocandoZombis = true;
            InvokeRepeating("InvocarZombi", 0f, 10f);
        }

        if (vida <= 10 && !lanzandoBolas)
        {
            lanzandoBolas = true;
            InvokeRepeating("LanzarBolaDeFuego", 0f, 5f);
        }

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Crecer()
    {
        float factorCrecimiento = 1.1f; // Aumenta el tamaño en un 10%
        transform.localScale *= factorCrecimiento;
        Debug.Log("Zombi ha crecido. Nuevo localScale: " + transform.localScale);
    }

    void Morir()
    {
        Debug.Log("Zombi destruido");
        CancelInvoke("InvocarZombi");
        CancelInvoke("LanzarBolaDeFuego");

        // Espera un segundo para que se vea que muere y luego carga la escena
        Invoke("CargarEscenaVictoria", 1f);
        Destroy(gameObject, 1f); // Se destruye después de 1 segundo
    }

    void CargarEscenaVictoria()
    {
        SceneManager.LoadScene("win"); // Cambia "Victoria" por el nombre exacto de tu escena
    }


    void InvocarZombi()
    {
        if (zombiPrefab != null && puntoSpawn != null)
        {
            Instantiate(zombiPrefab, puntoSpawn.position, Quaternion.identity);
            Debug.Log("¡Zombi invocado!");
        }
    }

    void LanzarBolaDeFuego()
    {
        if (bolaDeFuegoPrefab != null && puntoDisparo != null)
        {
            // Aplica el offset vertical al punto de disparo
            Vector3 puntoAjustado = puntoDisparo.position + new Vector3(0f, offsetYBolaFuego, 0f);

            GameObject bola = Instantiate(bolaDeFuegoPrefab, puntoAjustado, Quaternion.identity);

            // Configurar la dirección
            bola_de_fuego_controller script = bola.GetComponent<bola_de_fuego_controller>();
            if (script != null)
            {
                script.SetDirection(direccion);
            }

            Debug.Log("¡Jefe lanzó bola de fuego hacia: " + direccion + "!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Pared"))
        {
            direccion = (direccion == "Derecha") ? "Izquierda" : "Derecha";
            Debug.Log("Cambió dirección a: " + direccion);
        }
    }
}
