using UnityEngine;

public class SwordHandler : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Dipanggil oleh SwordActivator setiap kali klik
    public void Slash()
    {
        animator.SetTrigger("Slash");
    }
}