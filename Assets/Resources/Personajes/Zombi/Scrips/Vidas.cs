using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vidas : MonoBehaviour
{
    public int vida = 3;  // Vidas iniciales del zombi

    // Método para recibir daño
    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;
        Debug.Log("Vida actual: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    // Método que destruye el zombi cuando muere
    void Morir()
    {
        Debug.Log("Zombi destruido");
        Destroy(gameObject);
    }
}
