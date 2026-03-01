using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour, ISavable
{
    public int capacity = 3;
    public List<ItemData> items;

    private void Awake()
    {
        items = new List<ItemData>();
    }

    public void SaveData(GameSaveData saveData)
    {
        if (saveData.fantasyData == null)
            saveData.fantasyData = new FantasySaveData();

        saveData.fantasyData.itemIds.Clear();

        foreach (ItemData item in items)
        {
            if (item != null && !string.IsNullOrEmpty(item.itemId))
            {
                saveData.fantasyData.itemIds.Add(item.itemId);
            }
        }
    }

    public void LoadData(GameSaveData saveData)
    {
        if (saveData.fantasyData == null)
            return;

        ItemDatabase database = FindFirstObjectByType<ItemDatabase>();

        if (database == null)
        {
            return;
        }

        items.Clear();

        foreach (string itemId in saveData.fantasyData.itemIds)
        {
            ItemData item = database.GetItemById(itemId);

            if (item != null && items.Count < capacity)
            {
                items.Add(item);
            }
        }

        InventorySystem.Instance.inventoryUI.Refresh(items);
    }
}
