using UnityEngine;
using UnityEngine.UI;

public class SlotComponent : MonoBehaviour
{
    public Image icon;
    private ItemData item;

    public bool IsEmpty() => item == null;

    public void SetItem(ItemData newItem)
    {
        item = newItem;

        Debug.Log(
            $"Slot SetItem: {item.itemId}, " +
            $"Icon: {(item.icon != null ? item.icon.name : "NULL")}"
        );

        if (icon != null && item.icon != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
    }

    public void Clear()
    {
        item = null;
        if (icon != null)
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }
}
