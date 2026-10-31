using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("Inventory UI")]
    public GameObject inventoryCanvas;
    public Transform itemSlotContainer;    // parent tempat ItemSlot di-instantiate
    public GameObject itemSlotPrefab;
    public GameObject noItemTXT;

    [Header("Detail Panel")]
    public Image itemIcon;
    public TextMeshProUGUI itemNameTXT;
    public TextMeshProUGUI itemQtyTXT;
    public TextMeshProUGUI itemDescTXT;

    private const int MAX_STACK = 99;

    private class Entry
    {
        public ItemData data;
        public int quantity;
        public InventorySlot slotUI;
    }

    private List<Entry> entries = new List<Entry>();
    private InventorySlot selectedSlot;

    private InputAction inventoryAction;
    private InputAction deselectAction;
    private bool inventoryActive = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryCanvas.SetActive(false);
        inventoryAction = InputSystem.actions.FindAction("Inventory");
        deselectAction  = InputSystem.actions.FindAction("DeselectItem");
        inventoryActive = false;

        ClearDetailPanel();
        RefreshNoItemTXT();
    }

    private void Update()
    {
        if (inventoryAction.triggered && inventoryCanvas != null)
        {
            if (!inventoryActive) OpenInventory();
            else                  CloseInventory();
        }

        if (inventoryActive && deselectAction.triggered)
            DeselectCurrent();
    }

    private void OpenInventory()
    {
        inventoryActive = true;
        Time.timeScale = 0f;
        inventoryCanvas.SetActive(true);

        // Default: item pertama selected (kalau ada item & belum ada yang dipilih)
        if (selectedSlot == null && entries.Count > 0)
            SelectSlot(entries[0].slotUI);
    }

    private void CloseInventory()
    {
        inventoryActive = false;
        Time.timeScale = 1f;
        inventoryCanvas.SetActive(false);
    }

    // ================= ADD ITEM =================
    public bool AddItem(ItemData data, int qty)
    {
        Entry entry = entries.Find(e => e.data == data);

        if (entry != null)
        {
            if (entry.quantity >= MAX_STACK)
            {
                Debug.Log("Inventory sudah penuh!");
                return false;
            }

            int space = MAX_STACK - entry.quantity;
            int toAdd = Mathf.Min(space, qty);
            entry.quantity += toAdd;
            entry.slotUI.UpdateQuantity(entry.quantity);

            if (toAdd < qty) Debug.Log("Inventory sudah penuh!");
            if (selectedSlot == entry.slotUI) ShowDetail(entry.data, entry.quantity);
        }
        else
        {
            int toAdd = Mathf.Min(qty, MAX_STACK);

            GameObject slotObj = Instantiate(itemSlotPrefab, itemSlotContainer);
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.Setup(data, toAdd, this);

            entries.Add(new Entry { data = data, quantity = toAdd, slotUI = slot });

            if (toAdd < qty) Debug.Log("Inventory sudah penuh!");
        }

        RefreshNoItemTXT();
        return true;
    }

    // ================= SELECT / DESELECT =================
    public void SelectSlot(InventorySlot slot)
    {
        if (selectedSlot != null) selectedSlot.SetSelected(false);

        selectedSlot = slot;
        selectedSlot.SetSelected(true);

        Entry entry = entries.Find(e => e.slotUI == slot);
        if (entry != null) ShowDetail(entry.data, entry.quantity);
    }

    public void DeselectCurrent()
    {
        if (selectedSlot != null)
        {
            selectedSlot.SetSelected(false);
            selectedSlot = null;
        }
        ClearDetailPanel();
    }

    // ================= DETAIL PANEL =================
    private void ShowDetail(ItemData data, int qty)
    {
        itemIcon.sprite = data.itemIcon;
        itemIcon.enabled = true;
        itemNameTXT.text = data.itemName;
        itemQtyTXT.text = qty.ToString();
        itemDescTXT.text = data.itemDescription;
    }

    private void ClearDetailPanel()
    {
        itemIcon.sprite = null;
        itemIcon.enabled = false;
        itemNameTXT.text = "";
        itemQtyTXT.text = "";
        itemDescTXT.text = "";
    }

    private void RefreshNoItemTXT()
    {
        noItemTXT.SetActive(entries.Count == 0);
    }
}