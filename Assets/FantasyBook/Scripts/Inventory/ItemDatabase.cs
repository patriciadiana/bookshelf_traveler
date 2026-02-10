using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<ItemData> allItems;

    private Dictionary<string, ItemData> lookup;

    private void Awake()
    {
        lookup = new Dictionary<string, ItemData>();

        foreach (ItemData item in allItems)
        {
            if (!string.IsNullOrEmpty(item.itemId))
            {
                lookup[item.itemId] = item;
            }
        }
    }

    public ItemData GetItemById(string id)
    {
        return lookup.TryGetValue(id, out var item) ? item : null;
    }
}
