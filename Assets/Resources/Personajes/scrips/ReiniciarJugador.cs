using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para recargar la escena
using System.IO;

public class ReiniciarJugador : MonoBehaviour
{
    private AudioSource audioGameOver;
    private AudioSource musicaFondo;
    private bool yaSonado = false;
    string rutaGuardado;

    void Start()
    {
        rutaGuardado = Path.Combine(Application.persistentDataPath, "partida.json");
        audioGameOver = GetComponent<AudioSource>();

        // Buscar el objeto de música por tag o nombre
        GameObject objMusica = GameObject.FindWithTag("Music");
        if (objMusica != null)
        {
            musicaFondo = objMusica.GetComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("GameOver") && !yaSonado)
        {
            // Elimina archivo viejo si quieres empezar desde cero
            if (File.Exists(rutaGuardado))
            {
                File.Delete(rutaGuardado);
                
            }

            yaSonado = true;

            // Pausar la música de fondo
            if (musicaFondo != null)
            {
                musicaFondo.Pause();
            }

            // Reproducir sonido Game Over
            if (audioGameOver != null)
                audioGameOver.Play();

            // Reiniciar la escena después de que termine el sonido (1 segundo de delay aprox)
            GameManager.vidas = 1;
            Invoke("ReiniciarEscena", 1f);
        }
    }

    void ReiniciarEscena()
    {
        // Recarga la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
