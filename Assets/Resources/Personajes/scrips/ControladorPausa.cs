using UnityEngine;

public class ControladorPausa : MonoBehaviour
{
    public GameObject panelPausa;
    private bool juegoPausado = false;
    private AudioSource musicaFondo;

    private GuardadoManager guardado; // 🔹 Referencia al gestor de guardado

    void Start()
    {
        guardado = FindObjectOfType<GuardadoManager>(); // 🔹 Busca el script en la escena

        GameObject objMusica = GameObject.FindWithTag("Music");
        if (objMusica != null)
        {
            musicaFondo = objMusica.GetComponent<AudioSource>();
        }

        panelPausa.SetActive(false);
    }

    public void PausarJuego()
    {
        panelPausa.SetActive(true);
        Time.timeScale = 0f;
        juegoPausado = true;

        if (musicaFondo != null)
            musicaFondo.Pause();
    }

    public void ReanudarJuego()
    {
        panelPausa.SetActive(false);
        Time.timeScale = 1f;
        juegoPausado = false;

        if (musicaFondo != null)
            musicaFondo.UnPause();
    }

    public void GuardarJuego()
    {
        if (guardado != null)
        {
            guardado.GuardarPartida(); // 🔹 Llama al método de guardado
        }
    }

    public void SalirDelJuego()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
