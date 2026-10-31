using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Pengaturan Spawn")]
    [Tooltip("Masukkan prefab musuh ke sini")]
    public GameObject enemyPrefab;
    [Tooltip("Jeda antar kemunculan musuh (detik)")]
    public float spawnInterval = 4f;

    void Start()
    {
        // Memanggil fungsi SpawnEnemy secara berulang
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Memunculkan musuh di posisi dan rotasi spawner ini
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }
}