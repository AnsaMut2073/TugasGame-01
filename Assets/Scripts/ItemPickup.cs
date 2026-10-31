using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;
    public int quantity = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        bool picked = InventoryManager.instance.AddItem(itemData, quantity);
        if (picked) Destroy(gameObject);
    }
}