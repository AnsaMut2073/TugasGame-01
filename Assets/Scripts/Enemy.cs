using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [Header("Pengaturan State Machine")]
    [SerializeField] private float jarakDeteksi = 6f; // masuk CHASE
    [SerializeField] private float radiusPatrol = 3f; // area keliling PATROL

    // state sekarang -- mulai dari IDLE
    private StateZombie state = StateZombie.IDLE;

    // Variabel untuk PATROL
    private Vector2 titikAwal; // pusat area keliling
    private Vector2 tujuanPatrol; // titik yang sedang dituju

    // Kecepatan gerak musuh
    public float ms = 2f;

    // HP musuh
    public int hp = 1;

    protected Transform player;

    protected virtual void Start()
    {
        // Cari player
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Catat posisi awal untuk PATROL
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
        }
    }

    void PerilakuIdle() 
    { 
        // Diam sebentar - bisa ditambahin animasi nanti
    }

    void PerilakuPatrol() 
    { 
        // gerakan patrol
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

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakDeteksi)
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
}