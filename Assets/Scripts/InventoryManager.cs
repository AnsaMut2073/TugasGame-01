using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Input System")]
    private InputAction inventoryAction;

    [Header("UI & Inventory Setup")]
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private int slotCount = 16;

    private bool isInventoryActive;
    private ItemSlot[] itemSlots;

    private void Awake()
    {
        // Cari Action "Inventory" dari Input System
        inventoryAction = InputSystem.actions?.FindAction("Inventory");
    }

    private void OnEnable()
    {
        if (inventoryAction != null)
        {
            inventoryAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (inventoryAction != null)
        {
            inventoryAction.Disable();
        }
    }

    private void Start()
    {
        if (inventoryCanvas == null)
        {
            Debug.LogError("inventoryCanvas belum di-assign pada Inspector!", this);
            return;
        }

        inventoryCanvas.SetActive(false);
        isInventoryActive = false;

        CreateSlots();
    }

    private void Update()
    {
        if (inventoryAction == null) return;

        if (inventoryAction.triggered)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;
        inventoryCanvas.SetActive(isInventoryActive);

        // Freeze atau Unfreeze jalannya permainan saat UI Inventory terbuka
        Time.timeScale = isInventoryActive ? 0f : 1f;

        Debug.Log("Inventory " + (isInventoryActive ? "Opened" : "Closed"));
    }

    private void CreateSlots()
    {
        ItemSlot slotPrefab = Resources.Load<ItemSlot>("ItemSlot");
        if (slotPrefab == null)
        {
            Debug.LogError("Prefab 'ItemSlot' tidak ditemukan di dalam folder Resources!");
            itemSlots = new ItemSlot[0];
            return;
        }

        Transform parent = inventoryCanvas.transform;
        GridLayoutGroup grid = inventoryCanvas.GetComponentInChildren<GridLayoutGroup>(true);
        if (grid != null)
        {
            parent = grid.transform;
        }

        // Hapus child bawaan (jika ada) agar tidak terjadi duplikasi slot di UI Grid
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }

        itemSlots = new ItemSlot[slotCount];
        for (int i = 0; i < slotCount; i++)
        {
            itemSlots[i] = Instantiate(slotPrefab, parent);
            itemSlots[i].ClearSlot(); // Sembunyikan icon/text awal
        }
    }

    public void AddItem(ItemDetails item)
    {
        if (itemSlots == null || itemSlots.Length == 0)
        {
            Debug.LogError("Slot inventoris belum terinisialisasi!", this);
            return;
        }

        if (item == null) return;

        // Buat instance/copy data agar tidak mengubah jumlah asli ScriptableObject secara tidak sengaja
        ItemDetails remainingItem = Instantiate(item);

        // Step 1: Cari slot yang sudah terisi item berjenis SAMA untuk ditumpuk (Stack)
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].isFull && itemSlots[i].ItemName == remainingItem.ItemName)
            {
                itemSlots[i].AddItem(remainingItem);
                Debug.Log($"Item berhasil ditambahkan (Stacked): {item.ItemName}");
                return;
            }
        }

        // Step 2: Jika belum ada item yang sama, cari slot yang KOSONG
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!itemSlots[i].isFull)
            {
                itemSlots[i].AddItem(remainingItem);
                Debug.Log($"Item berhasil dimasukkan ke slot baru: {item.ItemName}");
                return;
            }
        }

        // Step 3: Jika semua slot sudah terisi penuh
        Debug.LogWarning($"Inventoris penuh! Item {item.ItemName} tidak dapat dimasukkan.", this);
    }

    public void DeselectAllSlot()
    {
        if (itemSlots == null) return;

        foreach (ItemSlot slot in itemSlots)
        {
            if (slot != null)
            {
                slot.isItemSelected = false;
                
                if (slot.selectedPanel != null)
                {
                    slot.selectedPanel.SetActive(false);
                }
            }
        }
    }
}