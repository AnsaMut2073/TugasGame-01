using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    InputAction pauseAction;
    [SerializeField] private GameObject PausePanel;
    public int totalKoin;
    private int koinTerkumpul = 0;
    private bool PauseActive;

    void Start()
    {
        PausePanel.SetActive(false);
        pauseAction = InputSystem.actions.FindAction("Pause");

        // Hitung total koin yang ada di scene saat game baru dimulai
        totalKoin = GameObject.FindGameObjectsWithTag("Coin").Length;
        Debug.Log("Total koin di stage: " + totalKoin);
    }

    void Update()
    {
        if (pauseAction.triggered && PauseActive == false && PausePanel != null)
        {
            PauseActive = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        } else if (pauseAction.triggered && PauseActive == true && PausePanel != null)
        {
            PauseActive = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
        }
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