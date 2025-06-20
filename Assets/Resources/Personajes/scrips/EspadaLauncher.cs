using UnityEngine;
using UnityEngine.UI;

public class EspadaLauncher : MonoBehaviour
{
    public AudioClip sonidoLanzarEspada;
    private AudioSource audioSource;

    public GameObject espadaPrefab;
    public Transform puntoLanzamiento;
    public int espadasDisponibles = 10;
    public Text textoNumEspadas;

    private string direccion = "Derecha";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        ActualizarTextoEspadas();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            direccion = "Derecha";
        else if (Input.GetKey(KeyCode.LeftArrow))
            direccion = "Izquierda";

        if (Input.GetKeyDown(KeyCode.M) && espadasDisponibles > 0)
        {
            LanzarEspada();
        }
    }

    void LanzarEspada()
    {
        GameObject espada = Instantiate(espadaPrefab, puntoLanzamiento.position, Quaternion.identity);

        // Rotar la espada según la dirección
        if (direccion == "Izquierda")
        {
            espada.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        EspadaController scriptEspada = espada.GetComponent<EspadaController>();
        if (scriptEspada != null)
            scriptEspada.SetDirection(direccion);

        espadasDisponibles--;

        if (sonidoLanzarEspada != null && audioSource != null)
            audioSource.PlayOneShot(sonidoLanzarEspada);

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
            textoNumEspadas.text = espadasDisponibles.ToString();
        }
    }
}
