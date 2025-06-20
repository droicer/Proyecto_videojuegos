using UnityEngine;
using System.IO;

public class GeneradorDePersonaje : MonoBehaviour
{
    void Start()
    {
        // Buscar los personajes por nombre en la escena
        GameObject jugador1 = GameObject.Find("Player_1");
        GameObject jugador2 = GameObject.Find("Player_2");

        // Ruta del archivo que guarda la elección
        string ruta = Path.Combine(Application.persistentDataPath, "personaje.txt");

        if (File.Exists(ruta))
        {
            string contenido = File.ReadAllText(ruta);

            if (int.TryParse(contenido, out int numeroPersonaje))
            {
                if (numeroPersonaje == 1)
                {
                    if (jugador2 != null)
                        Destroy(jugador2);
                }
                else if (numeroPersonaje == 2)
                {
                    if (jugador1 != null)
                        Destroy(jugador1);
                }
                else
                {
                    Debug.LogWarning("Número de personaje inválido: " + numeroPersonaje);
                }
            }
            else
            {
                Debug.LogWarning("Contenido de personaje.txt no es un número válido.");
            }
        }
        else
        {
            Debug.LogWarning("Archivo personaje.txt no encontrado en: " + ruta);
        }
    }
}
