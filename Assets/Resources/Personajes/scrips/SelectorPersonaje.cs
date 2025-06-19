using UnityEngine;
using UnityEngine.UI;

public class SelectorPersonaje : MonoBehaviour
{
    public Sprite[] spritesPersonajes;
    public Image imagenPersonajeActual;

    private int indiceActual = 0;

    public static int personajeSeleccionado = 1;

    void Start()
    {
        ActualizarSprite(); // ✅ Muestra el primer personaje al arrancar
    }

    public void CambiarIzquierda()
    {
        indiceActual = (indiceActual - 1 + spritesPersonajes.Length) % spritesPersonajes.Length;
        ActualizarSprite();
    }

    public void CambiarDerecha()
    {
        indiceActual = (indiceActual + 1) % spritesPersonajes.Length;
        ActualizarSprite();
    }

    void ActualizarSprite()
    {
        if (imagenPersonajeActual != null)
        {
            imagenPersonajeActual.sprite = spritesPersonajes[indiceActual];
        }
        else
        {
            Debug.LogWarning("imagenPersonajeActual no asignada en el inspector.");
        }

        personajeSeleccionado = indiceActual + 1;
    }
}

