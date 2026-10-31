using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public Image itemIcon;
    public TextMeshProUGUI itemQtyTXT;
    public GameObject selectedItemPanel;

    private ItemData data;
    private int quantity;
    private InventoryManager manager;

    public ItemData Data => data;
    public int Quantity => quantity;

    public void Setup(ItemData itemData, int qty, InventoryManager invManager)
    {
        data = itemData;
        quantity = qty;
        manager = invManager;

        itemIcon.sprite = data.itemIcon;
        itemQtyTXT.text = quantity.ToString();
        SetSelected(false);
    }

    public void UpdateQuantity(int qty)
    {
        quantity = qty;
        itemQtyTXT.text = quantity.ToString();
    }

    public void SetSelected(bool selected)
    {
        selectedItemPanel.SetActive(selected);
    }

    // Klik kiri pada slot = select
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            manager.SelectSlot(this);
    }
}