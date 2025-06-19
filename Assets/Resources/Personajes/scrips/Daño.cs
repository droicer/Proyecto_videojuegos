using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class Daño : MonoBehaviour
{
    string rutaGuardado;

    public float reboteFuerza = 12f;
    public float invulnerabilidadTiempo = 1f;

    public AudioClip sonidoDaño;      // Sonido al recibir daño
    private AudioSource audioSource;  // Fuente de audio


    private bool puedeRecibirDaño = true;
    private Rigidbody2D rb;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;


        rutaGuardado = Path.Combine(Application.persistentDataPath, "partida.json");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Zombi")
            || collision.gameObject.CompareTag("bola_de_fuego")
            || collision.gameObject.CompareTag("Jefe"))
            && puedeRecibirDaño)

        {
            // Reproducir sonido
            if (sonidoDaño != null)
                audioSource.PlayOneShot(sonidoDaño);

            // Rebote hacia arriba
            rb.velocity = new Vector2(rb.velocity.x, reboteFuerza);

            // Quitar una vida
            GameManager.vidas--;

            // Verificar si quedan vidas
            if (GameManager.vidas <= 0)
            {
                if (File.Exists(rutaGuardado))
                {
                    File.Delete(rutaGuardado);
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(Invulnerabilidad());
                FindObjectOfType<GameManager>().SendMessage("ActualizarUI");
            }
        }

    }

    IEnumerator Invulnerabilidad()
    {
        puedeRecibirDaño = false;
        yield return new WaitForSeconds(invulnerabilidadTiempo);
        puedeRecibirDaño = true;
    }
}
