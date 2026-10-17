using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Configuration")]
    [SerializeField] private ItemDetails itemDetails; // Mengubah nama variabel ke huruf kecil awal (camelCase)

    private void Awake()
    {
        // Validasi data di awal agar kamu langsung tahu kalau ada prefab item yang lupa diisi data ItemDetails-nya
        if (itemDetails == null)
        {
            Debug.LogWarning($"ItemDetails belum di-assign pada GameObject '{gameObject.name}'!", this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pastikan objek yang menabrak adalah Player
        if (!other.CompareTag("Player")) return;

        // Pastikan data itemDetails tidak null sebelum diproses
        if (itemDetails == null)
        {
            Debug.LogError($"Item '{gameObject.name}' tidak memiliki ItemDetails yang valid!", this);
            return;
        }

        // Cari komponen GrabableItem langsung dari objek Player yang menabrak item ini
        if (other.TryGetComponent<GrabableItem>(out GrabableItem itemCollector))
        {
            // Tambahkan item ke kolektor/inventory
            itemCollector.AddItem(itemDetails);

            // Hancurkan GameObject item dari scene
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning($"Objek bertag 'Player' ({other.name}) menabrak item, tetapi tidak memiliki komponen 'GrabableItem'!", other);
        }
    }
}