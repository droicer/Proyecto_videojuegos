using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; // Importante para trabajar con Tilemaps

public class RecojerVidas : MonoBehaviour
{
    public Tilemap tilemap; // Referencia al Tilemap donde están los hongos

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador tocó el Tilemap (o un objeto que represente un hongo)
        if (collision.gameObject.CompareTag("Hongos"))
        {
            // Obtener la posición del contacto
            Vector3 hitPosition = Vector3.zero;
            if (collision.contacts.Length > 0)
            {
                hitPosition = collision.contacts[0].point;
            }

            // Convertir la posición del mundo a la celda del tilemap
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

            // Eliminar solo el tile en esa celda
            tilemap.SetTile(cellPosition, null);

            Debug.Log("¡Hongo recolectado!");
        }
    }
}
