using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel_1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Meta"))
        {
            SceneManager.LoadScene("Nivel2"); // Cambia "Escena2" por el nombre real de tu escena
        }
    }
}
