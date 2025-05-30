using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int vidas;
    private int vidasIniciales = 1; // Puedes poner 3 o lo que necesites
    public Text Num_Vidas;

    void Start()
    {
        if (vidas <= 0) // Solo asigna si no hay valor previo cargado
            vidas = vidasIniciales;

        ActualizarUI();
    }


    public void AumentarVida()
    {
        vidas++;
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        Num_Vidas.text = "Vidas: " + vidas;
    }
}
