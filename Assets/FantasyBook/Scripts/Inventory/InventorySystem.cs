using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    public InventoryUIController inventoryUI;

    public static void AddItem(InventoryComponent inventory, ItemComponent item)
    {
        if(inventory.items.Count < inventory.capacity)
        {
            inventory.items.Add(item);

            if (Instance.inventoryUI != null)
            {
                Instance.inventoryUI.AddItem(item.itemPrefab);
            }
            else
            {
                Debug.Log("UI or prefab is null!");
            }
        }
        else
        {
            Debug.Log("Inventory is full");
        }
    }
}
