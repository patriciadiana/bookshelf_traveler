using NUnit.Framework.Interfaces;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;

    private void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            InventoryItem itemData = collision.GetComponent<InventoryItem>();

            if (itemData == null)
            {
                Debug.LogWarning("Item has no ItemData!");
                return;
            }

            bool added = inventoryController.AddItem(itemData.itemUIPrefab);

            if (added)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
