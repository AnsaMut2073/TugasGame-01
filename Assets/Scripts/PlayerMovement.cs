using UnityEngine;
using UnityEngine.InputSystem; // WAJIB untuk Input System Unity 6

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [SerializeField] private int hp = 100;
    
    public void KenaDamage(int damage)
    {
        hp -= damage;
        Debug.Log("Player kena damage! HP: " + hp);
        
        if (hp <= 0)
        {
            // Logika mati player
        }
    }

    public float kecepatan = 5f;
    public int skor = 0;

    private Vector2 arahGerak; // menyimpan nilai dari action "Move"
    private GameManager gameManager; // referensi ke GameManager

    void Start()
    {
        // Mencari GameManager yang ada di scene
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Dipanggil OTOMATIS oleh komponen Player Input (Action: Move)
    void OnMove(InputValue value)
    {
        // Mengambil nilai Vector2 dari input (WASD / Panah)
        arahGerak = value.Get<Vector2>();
    }

    void Update()
    {
        // Menggerakkan player dengan arahGerak, kecepatan, dan Time.deltaTime agar mulus
        Vector3 arah = new Vector3(arahGerak.x, arahGerak.y, 0);
        transform.position += arah * kecepatan * Time.deltaTime;
    }

    // Dipanggil otomatis saat Player menyentuh objek ber-Trigger (Koin)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa apakah objek yang disentuh memiliki tag "Coin"
        if (other.CompareTag("Coin"))
        {
            // Hancurkan koin yang tersentuh
            Destroy(other.gameObject);

            // Tambah skor sebanyak 1
            skor++;

            // Tampilkan skor ke Console
            Debug.Log("Skor Saat Ini: " + skor);

            // Beritahu GameManager bahwa 1 koin telah diambil
            if (gameManager != null)
            {
                gameManager.AmbilKoin();
            }
        }
    }
}