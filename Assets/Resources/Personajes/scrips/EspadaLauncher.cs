using UnityEngine;
using UnityEngine.UI; // Necesario para usar Text UI

public class EspadaLauncher : MonoBehaviour
{
    public AudioClip sonidoLanzarEspada; // Clip que se reproducirá
    private AudioSource audioSource;     // Fuente de audio

    public GameObject espadaPrefab;             // Prefab de la espada
    public Transform puntoLanzamiento;          // Punto desde donde se lanza la espada
    public int espadasDisponibles = 10;         // Número de espadas
    public Text textoNumEspadas;                // Texto UI para mostrar cuántas espadas quedan

    private string direccion = "Derecha";       // Dirección actual del jugador

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        ActualizarTextoEspadas();
    }


    void Update()
    {
        // Cambiar dirección (simula detección según teclas)
        if (Input.GetKey(KeyCode.RightArrow))
            direccion = "Derecha";
        else if (Input.GetKey(KeyCode.LeftArrow))
            direccion = "Izquierda";

        // Lanzar espada si se presiona L y quedan espadas
        if (Input.GetKeyDown(KeyCode.L) && espadasDisponibles > 0)
        {
            LanzarEspada();
        }
    }

    void LanzarEspada()
    {
        // Instanciar la espada
        GameObject espada = Instantiate(espadaPrefab, puntoLanzamiento.position, Quaternion.identity);

        // Configurar dirección
        EspadaController scriptEspada = espada.GetComponent<EspadaController>();
        if (scriptEspada != null)
            scriptEspada.SetDirection(direccion);

        // Restar una espada
        espadasDisponibles--;

        // Reproducir sonido
        if (sonidoLanzarEspada != null && audioSource != null)
            audioSource.PlayOneShot(sonidoLanzarEspada);

        // Actualizar el texto
        ActualizarTextoEspadas();
    }

    public void GanarEspadas(int cantidad)
    {
        espadasDisponibles += cantidad;
        ActualizarTextoEspadas();
    }



    void ActualizarTextoEspadas()
    {
        if (textoNumEspadas != null)
        {
            textoNumEspadas.text = "" + espadasDisponibles.ToString();
        }
    }
}
