using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel_2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Nivel completado!");
            SceneManager.LoadScene("Final");
        }
    }
}
