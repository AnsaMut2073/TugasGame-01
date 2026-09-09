using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;

    void Start()
    {
        // Hitung total koin yang ada di scene saat game baru dimulai
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("Total koin di stage: " + totalKoin);
    }

    public void AmbilKoin()
    {
        koinTerkumpul++;

        // Jika jumlah koin terkumpul sama dengan total koin di scene, panggil kondisi menang
        if (koinTerkumpul >= totalKoin)
        {
            Menang();
        }
    }

    void Menang()
    {
        Debug.Log("KAMU MENANG!");
    }
}