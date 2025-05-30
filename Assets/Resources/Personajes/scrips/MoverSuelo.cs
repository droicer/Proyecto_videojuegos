using UnityEngine;

public class MoverSuelo : MonoBehaviour
{
    public float velocidad = 2f;
    private bool activarMovimiento = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            activarMovimiento = true;
        }
    }

    void Update()
    {
        if (activarMovimiento)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        }
    }
}
