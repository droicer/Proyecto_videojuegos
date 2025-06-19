using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuPrincipal : MonoBehaviour
{
    string rutaGuardado;

    [System.Serializable]
    private class DatosPartida
    {
        public string nombreEscena;
        // Puedes agregar más datos aquí si los necesitas
    }

    void Start()
    {
        rutaGuardado = Path.Combine(Application.persistentDataPath, "partida.json");

        // Si no existe partida guardada, desactiva el botón "Continuar"
        if (!File.Exists(rutaGuardado))
        {
            GameObject btnContinuar = GameObject.Find("BtnContinuar");
            if (btnContinuar != null)
                btnContinuar.SetActive(false);
        }
    }

    public void ContinuarPartida()
    {
        if (!File.Exists(rutaGuardado))
        {
            Debug.LogWarning("No hay partida guardada para continuar.");
            return;
        }

        GuardarSeleccionEnArchivo(); // 💾 Guardamos personaje antes de continuar

        string json = File.ReadAllText(rutaGuardado);
        DatosPartida datos = JsonUtility.FromJson<DatosPartida>(json);

        if (!string.IsNullOrEmpty(datos.nombreEscena))
        {
            Debug.Log("Cargando escena guardada: " + datos.nombreEscena);
            SceneManager.LoadScene(datos.nombreEscena);
        }
        else
        {
            Debug.LogWarning("No se encontró el nombre de la escena en el archivo guardado.");
        }
    }

    public void NuevaPartida()
    {
        if (File.Exists(rutaGuardado))
        {
            File.Delete(rutaGuardado);
        }

        GuardarSeleccionEnArchivo(); // 💾 Guardamos personaje antes de empezar

        // Cambia "SampleScene" por el nombre real de tu escena inicial
        SceneManager.LoadScene("SampleScene");
    }

    public void SalirDelJuego()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void GuardarSeleccionEnArchivo()
    {
        // Tomamos la selección actual del personaje desde el script SelectorPersonaje
        int numero = SelectorPersonaje.personajeSeleccionado;

        string ruta = Path.Combine(Application.persistentDataPath, "personaje.txt");
        File.WriteAllText(ruta, numero.ToString());

        Debug.Log("Guardado personaje seleccionado: " + numero);
    }
}
