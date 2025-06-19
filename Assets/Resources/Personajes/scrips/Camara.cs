using UnityEngine;

public class Camara : MonoBehaviour
{
    public Transform objetivo; // El personaje a seguir
    public float suavizado = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        // 🔍 Si aún no tenemos objetivo, lo buscamos
        if (objetivo == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                objetivo = player.transform;
            }
            else
            {
                return; // Aún no hay jugador, no hacemos nada
            }
        }

        // 🎥 Movimiento suave de cámara
        Vector3 posicionDeseada = objetivo.position + offset;
        Vector3 posicionSuavizada = Vector3.Lerp(transform.position, posicionDeseada, suavizado);
        transform.position = new Vector3(posicionSuavizada.x, posicionSuavizada.y, transform.position.z);
    }
}
