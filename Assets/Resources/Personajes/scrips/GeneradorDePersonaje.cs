using UnityEngine;
using System.IO;

public class GeneradorDePersonaje : MonoBehaviour
{
    public GameObject prefabJugador1;  // El objeto ya presente en escena (ej. hombre)
    public GameObject prefabJugador2;  // El otro personaje en escena (ej. mujer)

    void Start()
    {
        string ruta = Path.Combine(Application.persistentDataPath, "personaje.txt");

        if (File.Exists(ruta))
        {
            string contenido = File.ReadAllText(ruta);

            if (int.TryParse(contenido, out int numeroPersonaje))
            {
                // Eliminar el que NO fue elegido
                if (numeroPersonaje == 1)
                {
                    if (prefabJugador2 != null)
                        Destroy(prefabJugador2);
                }
                else if (numeroPersonaje == 2)
                {
                    if (prefabJugador1 != null)
                        Destroy(prefabJugador1);
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
