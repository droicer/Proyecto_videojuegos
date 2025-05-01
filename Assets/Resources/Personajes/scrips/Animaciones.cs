using UnityEngine;

public class Animaciones : MonoBehaviour
{
    private Animator animator;
    private float movimiento;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movimiento = Input.GetAxisRaw("Horizontal");

        // Si se está moviendo
        if (movimiento != 0)
        {
            animator.SetBool("Caminar", false);
            animator.SetBool("Parar", true);
        }
        else // Si está quieto
        {
            animator.SetBool("Caminar", true);
            animator.SetBool("Parar", false);
        }
    }
}
