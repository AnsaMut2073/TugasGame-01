using UnityEngine;
using UnityEngine.InputSystem; // WAJIB untuk Input System Unity 6

// Tinggal tambahkan , GrabableItem di baris kelas
public class PlayerMovement : MonoBehaviour, GrabableItem
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";

    public int skor = 0;
    
    [Header("Managers")]
    private GameManager gameManager; // referensi ke GameManager
    [SerializeField] private InventoryManager inventoryManager; // referensi ke InventoryManager

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Mencari GameManager yang ada di scene
        gameManager = FindFirstObjectByType<GameManager>();

        // Mencari InventoryManager otomatis kalau lupa di-assign di Inspector
        if (inventoryManager == null)
        {
            inventoryManager = FindFirstObjectByType<InventoryManager>();
        }
    }

    void Update()
    {
        // Logika movement & animasi player
        
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.linearVelocity = _movement * _moveSpeed;

        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }
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

    // --- FUNGSI DARI INTERFACE GrabableItem ---
    public void AddItem(ItemDetails itemDetails)
    {
        if (inventoryManager != null)
        {
            inventoryManager.AddItem(itemDetails);
        }
        else
        {
            Debug.LogWarning("InventoryManager tidak ditemukan saat Player mencoba menambah item!", this);
        }
    }
}