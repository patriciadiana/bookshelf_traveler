using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject currentItem;

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public void SetItem(GameObject itemPrefab)
    {
        currentItem = Instantiate(itemPrefab, transform);
        currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void ClearSlot()
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
            currentItem = null;
        }
    }
}
