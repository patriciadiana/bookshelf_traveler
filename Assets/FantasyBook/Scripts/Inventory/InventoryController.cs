using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;

    private List<InventorySlot> slots = new List<InventorySlot>();

    private void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<InventorySlot>();
            slots.Add(slot);
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            if (slots[i].IsEmpty())
            {
                slots[i].SetItem(itemPrefab);
                return true;
            }
        }

        Debug.Log("Inventory full!");
        return false;
    }
}
