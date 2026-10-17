using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item data (Sesuai skrip dari gurumu)
    public string ItemName { get; private set; }
    public int ItemQuantity { get; private set; }
    public Sprite ItemIcon { get; private set; }
    public bool isFull;

    // Item slot UI elements (Sesuai skrip dari gurumu)
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    [SerializeField] private int maxStackSize = 99;

    // Selected item UI & State (Sesuai keinginanmu)
    public GameObject selectedPanel;
    public bool isItemSelected;

    public void AddItem(ItemDetails itemDetails)
    {
        if (itemDetails == null)
        {
            Debug.LogWarning("Tried to add null ItemDetails to slot.", this);
            return;
        }

        // Stack jika item sama, kalau beda/baru diisi set data baru
        if (isFull && ItemName == itemDetails.ItemName)
        {
            ItemQuantity += itemDetails.Quantity;
        }
        else
        {
            ItemName = itemDetails.ItemName;
            ItemQuantity = itemDetails.Quantity;
            ItemIcon = itemDetails.ItemIcon;
        }

        isFull = true;

        if (quantityText != null)
        {
            quantityText.text = ItemQuantity.ToString();
            quantityText.enabled = ItemQuantity > 1;
        }

        if (itemImage != null)
        {
            itemImage.sprite = ItemIcon;
            itemImage.enabled = ItemIcon != null;
        }
    }

    public void ClearSlot()
    {
        ItemName = null;
        ItemQuantity = 0;
        ItemIcon = null;
        isFull = false;

        // Reset status seleksi saat slot dibersihkan
        DeselectSlot();

        if (quantityText != null) 
        {
            quantityText.text = "";
            quantityText.enabled = false;
        }

        if (itemImage != null) 
        {
            itemImage.sprite = null;
            itemImage.enabled = false; // Mencegah tampilan kotak putih di UI
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            LeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick();
        }
    }

    private void LeftClick()
    {
        Debug.Log($"Left clicked on: {ItemName}");

        // Panggil fungsi InventoryManager untuk mematikan seleksi slot lain lebih dulu (opsional)
        InventoryManager inventoryManager = FindFirstObjectByType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.DeselectAllSlot();
        }

        // Aktifkan seleksi pada slot ini
        SelectSlot();
    }

    private void RightClick()
    {
        Debug.Log($"Right clicked on: {ItemName}");
        // Tambahkan fungsi klik kanan di sini jika dibutuhkan (misal: Pakai Item)
    }

    // Fungsi khusus untuk Select
    public void SelectSlot()
    {
        isItemSelected = true;
        if (selectedPanel != null)
        {
            selectedPanel.SetActive(true);
        }
    }

    // Fungsi khusus untuk Deselect
    public void DeselectSlot()
    {
        isItemSelected = false;
        if (selectedPanel != null)
        {
            selectedPanel.SetActive(false);
        }
    }
}