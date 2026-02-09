using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public int capacity = 3;
    public List<ItemComponent> items;

    private void Awake()
    {
        items = new List<ItemComponent>();
    }
}
