using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f; // masuk CHASE
    [SerializeField] private float jarakSerang = 1.2f; // masuk ATTACK
    [SerializeField] private float jedaSerang = 1f; // detik antar serang
    [SerializeField] private float radiusPatrol = 3f; // area keliling PATROL

    // state sekarang -- mulai dari IDLE
    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    // Variabel untuk PATROL (dari kode kamu)
    private Vector2 titikAwal; // pusat area keliling
    private Vector2 tujuanPatrol; // titik yang sedang dituju

    [Header("Stats")]
    [SerializeField] public int hp = 100;
    public float ms = 2f;

    [Header("Damage")]
    [SerializeField] private int damageSaatTabrakan = 10; // dari kode guru

    protected Transform player;

    // EVENT: dari kode guru
    public static event Action<Enemy> OnZombieMati;

    protected virtual void Start()
    {
        // Cari player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Catat posisi awal untuk PATROL (dari kode kamu)
        titikAwal = transform.position;
        PilihTujuanPatrolBaru();
    }

    void Update()
    {
        // LANGKAH A: tentukan state (aturan pindah)
        PeriksaTransisi();

        // LANGKAH B: jalankan perilaku sesuai state sekarang
        switch (state)
        {
            case StateZombie.IDLE: PerilakuIdle(); break;
            case StateZombie.PATROL: PerilakuPatrol(); break;
            case StateZombie.CHASE: PerilakuChase(); break;
            case StateZombie.ATTACK: PerilakuAttack(); break;
        }
    }

    void PerilakuIdle() 
    { 
        // Diam sebentar - bisa ditambahin animasi nanti
    }

    void PerilakuPatrol() 
    { 
        // ===== DARI KODE KAMU (gerakan patrol) =====
        transform.position = Vector2.MoveTowards(
            transform.position,
            tujuanPatrol,
            ms * 0.5f * Time.deltaTime
        );

        // Kalau sudah sampai tujuan, cari tujuan baru
        if (Vector2.Distance(transform.position, tujuanPatrol) < 0.1f)
        {
            PilihTujuanPatrolBaru();
        }
    }

    void PerilakuChase() 
    { 
        Kejar(); 
    }

    void PerilakuAttack()
    {
        // menyerang berkala, bukan setiap frame
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        }
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang)
        {
            state = StateZombie.ATTACK;
        }
        else if (jarak <= jarakDeteksi)
        {
            state = StateZombie.CHASE;
        }
        else
        {
            state = StateZombie.PATROL;
        }
    }

    // Method untuk mengukur jarak ke player
    float JarakKePlayer()
    {
        if (player == null) return Mathf.Infinity;
        return Vector2.Distance(transform.position, player.position);
    }

    // ===== DARI KODE KAMU =====
    void PilihTujuanPatrolBaru()
    {
        Vector2 acak = UnityEngine.Random.insideUnitCircle * radiusPatrol;
        tujuanPatrol = titikAwal + acak;
    }

    public void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            ms * Time.deltaTime
        );
    }

    public virtual void Serang()
    {
        Debug.Log(name + " menyerang!");
        // Nanti akan diisi dengan logika serang yang sebenarnya
    }

    // ===== DARI KODE GURU (Damage System) =====
    public void KenaDamage(int damage)
    {
        hp -= damage;
        Debug.Log(name + " kena damage: " + damage + ", HP sekarang: " + hp);
        
        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log(name + " mati!");
        OnZombieMati?.Invoke(this); // event dipanggil
        Destroy(gameObject);
    }

    // ===== DARI KODE GURU (Tabrakan dengan Player) =====
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable playerScript = other.GetComponent<IDamageable>();
            if (playerScript != null)
            {
                playerScript.KenaDamage(damageSaatTabrakan);
            }
        }
    }

    // ===== DARI KODE GURU (Testing di Inspector) =====
    [ContextMenu("Tes Event: Zombie Mati")]
    void TesEventMati()
    {
        Mati();
    }
}