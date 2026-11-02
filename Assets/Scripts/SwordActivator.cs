using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordActivator : MonoBehaviour
{
    [Header("Referensi")]
    [Tooltip("Seret GameObject SwordHandler ke sini")]
    [SerializeField] private GameObject swordHandler;

    [Tooltip("Seret action 'Attack' dari Input Actions asset ke sini")]
    [SerializeField] private InputActionReference attackAction;

    [Header("Pengaturan")]
    [Tooltip("Jeda nganggur sebelum pedang hilang lagi (detik)")]
    [SerializeField] private float hideDelay = 1.5f;

    private bool isActive = false;
    private Coroutine hideCoroutine;

    void Start()
    {
        // Sembunyikan pedang saat game mulai
        swordHandler.SetActive(false);
    }

    void OnEnable()
    {
        if (attackAction != null)
            attackAction.action.performed += OnAttack;
    }

    void OnDisable()
    {
        if (attackAction != null)
            attackAction.action.performed -= OnAttack;
    }

    void OnAttack(InputAction.CallbackContext ctx)
    {
        // Kalau pedang lagi sembunyi, nyalain dulu
        if (!isActive)
        {
            swordHandler.SetActive(true);
            isActive = true;
        }

        // Perintahkan slash
        swordHandler.GetComponent<SwordHandler>().Slash();

        // Reset timer sembunyi
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);
        swordHandler.SetActive(false);
        isActive = false;
        hideCoroutine = null;
    }
}