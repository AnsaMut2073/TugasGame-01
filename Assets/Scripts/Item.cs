using UnityEngine;

public class Item : MonoBehaviour
{
    ItemDetails itemDetails;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with item: " + itemDetails.itemName);
            GrabItem();
        }
    }

    private void GrabItem()
    {
        // Implementation for grabbing the item
        Debug.Log("Item grabbed: " + itemDetails.itemName);
        Destroy(gameObject); // Remove the item from the scene after grabbing
    }
}
