using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>
{
    public InventoryUIController inventoryUI;

    public static void AddItem(InventoryComponent inventory, ItemData item)
    {
        if (inventory.items.Count >= inventory.capacity)
        {
            return;
        }

        inventory.items.Add(item);
        Instance.inventoryUI?.AddItem(item);

        Instance.inventoryUI.Refresh(inventory.items);
    }
}
