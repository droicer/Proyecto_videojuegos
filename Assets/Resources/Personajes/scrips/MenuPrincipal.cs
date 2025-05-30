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
        // Otros datos que puedas tener
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
        // Elimina archivo viejo si quieres empezar desde cero
        if (File.Exists(rutaGuardado))
        {
            File.Delete(rutaGuardado);
        }

        // Cambia "EscenaJuego" por la escena inicial que quieras para nueva partida
        SceneManager.LoadScene("SampleScene");
    }

    public void SalirDelJuego()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
