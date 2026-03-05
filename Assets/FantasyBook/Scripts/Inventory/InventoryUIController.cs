using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;

    private List<SlotComponent> slots = new List<SlotComponent>();

    private void Start()
    {
        for(int i = 0; i < slotCount; i++)
        {
            SlotComponent slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<SlotComponent>();
            slots.Add(slot);
        }   
    }

    public bool AddItem(ItemData item)
    {
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            if (slots[i].IsEmpty())
            {
                slots[i].SetItem(item);
                return true;
            }
        }

        return false;
    }

    public void Refresh(List<ItemData> items)
    {
        foreach (var slot in slots)
            slot.Clear();


        for (int i = 0; i < items.Count && i < slots.Count; i++)
        {
            slots[slots.Count - 1 - i].SetItem(items[i]);
        }
    }

}
