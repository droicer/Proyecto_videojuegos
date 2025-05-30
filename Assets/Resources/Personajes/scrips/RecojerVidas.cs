using UnityEngine;
using UnityEngine.Tilemaps;

public class RecojerVidas : MonoBehaviour
{
    public Tilemap tilemap;
    public AudioClip sonidoRecolectarVida;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hongos"))
        {
            Vector3 hitPosition = collision.contacts[0].point;
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);
            tilemap.SetTile(cellPosition, null);

            Debug.Log("¡Hongo recolectado!");

            // Aumentar vidas en GameManager
            FindObjectOfType<GameManager>().AumentarVida();

            // Reproducir sonido
            if (sonidoRecolectarVida != null && audioSource != null)
                audioSource.PlayOneShot(sonidoRecolectarVida);
        }
    }
}
