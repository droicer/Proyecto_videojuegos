using UnityEngine;
using UnityEngine.UI;

public class SelectorPersonaje : MonoBehaviour
{
    public Sprite[] spritesPersonajes;        // Lista de sprites (2 en tu caso)
    public Image imagenPersonajeActual;       // UI Image donde se muestra el personaje

    private int indiceActual = 0;             // Índice del personaje mostrado

    public void CambiarIzquierda()
    {
        indiceActual--;
        if (indiceActual < 0)
            indiceActual = spritesPersonajes.Length - 1;

        ActualizarSprite();
    }

    public void CambiarDerecha()
    {
        indiceActual++;
        if (indiceActual >= spritesPersonajes.Length)
            indiceActual = 0;

        ActualizarSprite();
    }

    void ActualizarSprite()
    {
        imagenPersonajeActual.sprite = spritesPersonajes[indiceActual];
    }

    public int ObtenerPersonajeSeleccionado()
    {
        return indiceActual;
    }
}
