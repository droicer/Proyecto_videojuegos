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
    public float offsetYBolaFuego = -0.3f;

    private bool lanzandoBolas = false;
    private string direccion = "Derecha";

    public AudioClip sonidoBolaDeFuego;   // Clip de sonido
    private AudioSource audioSource;      // Fuente de audio

    void Start()
    {
        // Buscar o crear el componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

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
        float factorCrecimiento = 1.1f;
        transform.localScale *= factorCrecimiento;
        Debug.Log("Zombi ha crecido. Nuevo localScale: " + transform.localScale);
    }

    void Morir()
    {
        Debug.Log("Zombi destruido");
        CancelInvoke("InvocarZombi");
        CancelInvoke("LanzarBolaDeFuego");

        Invoke("CargarEscenaVictoria", 1f);
        Destroy(gameObject, 1f);
    }

    void CargarEscenaVictoria()
    {
        SceneManager.LoadScene("win");
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
            Vector3 puntoAjustado = puntoDisparo.position + new Vector3(0f, offsetYBolaFuego, 0f);
            GameObject bola = Instantiate(bolaDeFuegoPrefab, puntoAjustado, Quaternion.identity);

            bola_de_fuego_controller script = bola.GetComponent<bola_de_fuego_controller>();
            if (script != null)
                script.SetDirection(direccion);

            Debug.Log("¡Jefe lanzó bola de fuego hacia: " + direccion + "!");

            // Reproducir sonido
            if (sonidoBolaDeFuego != null && audioSource != null)
                audioSource.PlayOneShot(sonidoBolaDeFuego);
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
