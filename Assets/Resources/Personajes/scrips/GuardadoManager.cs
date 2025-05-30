using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardadoManager : MonoBehaviour
{
    private string rutaArchivo;

    void Awake()
    {
        rutaArchivo = Application.persistentDataPath + "/partida.json";
        CargarPartida();  // Cargar datos al iniciar la escena
    }

    public void GuardarPartida()
    {
        DatosPartida datos = new DatosPartida();

        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
            datos.posicionJugador = jugador.transform.position;

        datos.vidas = GameManager.vidas;

        EspadaLauncher lanzador = FindObjectOfType<EspadaLauncher>();
        if (lanzador != null)
            datos.espadas = lanzador.espadasDisponibles;

        datos.nombreEscena = SceneManager.GetActiveScene().name;

        string json = JsonUtility.ToJson(datos, true);
        File.WriteAllText(rutaArchivo, json);

        Debug.Log("Partida guardada en: " + rutaArchivo);
    }

    public void CargarPartida()
    {
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogWarning("No se encontró archivo de guardado.");
            return;
        }

        string json = File.ReadAllText(rutaArchivo);
        DatosPartida datos = JsonUtility.FromJson<DatosPartida>(json);

        GameObject jugador = GameObject.FindWithTag("Player");
        Debug.Log("Jugador encontrado: " + jugador);

        if (jugador != null)
            jugador.transform.position = datos.posicionJugador;

        GameManager.vidas = datos.vidas;

        EspadaLauncher lanzador = FindObjectOfType<EspadaLauncher>();
        if (lanzador != null)
        {
            lanzador.espadasDisponibles = datos.espadas;
            lanzador.SendMessage("ActualizarTextoEspadas");
        }

        GameManager gm = FindObjectOfType<GameManager>();
        if (gm != null)
        {
            gm.ActualizarUI();
        }

        Debug.Log("Partida cargada.");
    }

    public string ObtenerNombreEscenaGuardada()
    {
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogWarning("No se encontró archivo de guardado.");
            return null;
        }

        string json = File.ReadAllText(rutaArchivo);
        DatosPartida datos = JsonUtility.FromJson<DatosPartida>(json);

        return datos.nombreEscena;
    }
}
