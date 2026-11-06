using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    InputAction pauseAction;
    [SerializeField] private GameObject PausePanel;
    private bool PauseActive;

    void Start()
    {
        PausePanel.SetActive(false);
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    void Update()
    {
        if (pauseAction.triggered && PauseActive == false && PausePanel != null)
        {
            if (Cursor.visible == false)
            {
                Cursor.visible = true;
            }
            PauseActive = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
        } else if (pauseAction.triggered && PauseActive == true && PausePanel != null)
        {
            if (Cursor.visible == true)
            {
                Cursor.visible = false;
            }
            PauseActive = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
        }
    }
}