using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform objetivo; // El personaje a seguir
    public float suavizado = 0.125f; // Qué tan suave se mueve la cámara
    public Vector3 offset; // Para ajustar la posición de la cámara respecto al jugador

    void LateUpdate()
    {
        if (objetivo != null)
        {
            Vector3 posicionDeseada = objetivo.position + offset;
            Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
            transform.position = new Vector3(posicionSuavizada.x, posicionSuavizada.y, transform.position.z);
        }
    }
}
