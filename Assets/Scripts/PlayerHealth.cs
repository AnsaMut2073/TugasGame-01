using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Pengaturan HP")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI Healthbar")]
    [Tooltip("Seret GameObject HealthBar (Slider) ke sini")]
    public Slider healthBar;
    public TextMeshProUGUI textHP;

    void Start()
    {
        // Inisialisasi HP di awal game
        currentHealth = maxHealth;

        // Setup Slider di awal
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
        }

        UpdateHealthBar();
    }

    // Fungsi ini akan dipanggil oleh musuh saat menyerang
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // biar gak minus / lebih dari max

        UpdateHealthBar(); // ← Update UI setiap kena damage

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (textHP != null)
        {
            textHP.text = currentHealth.ToString();
        }
    }

    void Die()
    {
        Debug.Log("Player Mati!");
        // Di sini kamu bisa menambahkan logika game over, respawn, dll.
        // gameObject.SetActive(false);
    }
}