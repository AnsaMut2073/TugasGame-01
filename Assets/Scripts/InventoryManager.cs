using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    InputAction inventoryAction;
    [SerializeField] private GameObject inventoryCanvas;
    private bool isInventoryActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        isInventoryActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryAction.triggered && inventoryCanvas != null)
        {
            isInventoryActive = !isInventoryActive;
            inventoryCanvas.SetActive(isInventoryActive);
            Time.timeScale = isInventoryActive ? 0f : 1f; // Pause or resume the game
            Debug.Log(isInventoryActive ? "Inventory opened" : "Inventory closed");
        }
    }
}
