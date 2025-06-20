using UnityEngine;
using UnityEngine.UI;

public class ContadorZombis : MonoBehaviour
{
    public static ContadorZombis instancia;
    public Text textoZombis;
    private int totalMuertos = 0;

    void Awake()
    {
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    public void SumarZombi()
    {
        totalMuertos++;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (textoZombis != null)
        {
            textoZombis.text = "" + totalMuertos;
        }
    }

    public void EstablecerMuertes(int cantidad)
    {
        totalMuertos = cantidad;
        ActualizarTexto();
    }

    public int TotalMuertos()
    {
        return totalMuertos;
    }

}
