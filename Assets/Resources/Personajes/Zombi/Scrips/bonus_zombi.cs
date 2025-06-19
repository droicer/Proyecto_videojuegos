using UnityEngine;

public class bonus_zombi : MonoBehaviour
{
    public bool daEspadas = false;        //Solo si está en true, dará espadas
    public int espadasQueDa = 2;          // Cuántas espadas da si aplica

    void OnDestroy()
    {
        if (!daEspadas) return;           //Si no da espadas, salir

        EspadaLauncher lanzador = FindObjectOfType<EspadaLauncher>();
        if (lanzador != null)
        {
            lanzador.GanarEspadas(espadasQueDa);
            Debug.Log("Ganaste " + espadasQueDa + " espadas por matar este zombi");
        }
        else
        {
            Debug.LogWarning("No se encontró el script EspadaLauncher en la escena.");
        }
    }
}
