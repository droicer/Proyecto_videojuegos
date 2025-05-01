using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiniciarJugador : MonoBehaviour
{
    private Vector3 posicionInicial; // Guardar la posición inicial

    void Start()
    {
        // Guardamos la posición donde empieza el jugador
        posicionInicial = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {
            // Volver a la posición inicial
            transform.position = posicionInicial;
        }
    }
}
