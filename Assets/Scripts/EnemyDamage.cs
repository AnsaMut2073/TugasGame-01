using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Pengaturan Serangan")]
    public int damageAmount = 10;

    // Fungsi ini otomatis terpanggil saat musuh menyentuh trigger collider lain
    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek apakah yang disentuh adalah Player
        if (other.CompareTag("Player"))
        {
            // Coba ambil komponen PlayerHealth dari objek yang disentuh
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            
            if (playerHealth != null)
            {
                // Panggil fungsi TakeDamage di script PlayerHealth
                playerHealth.TakeDamage(damageAmount);
            }

            // (Opsional) Hancurkan musuh setelah menyerang
            Destroy(gameObject); 
        }
    }
}