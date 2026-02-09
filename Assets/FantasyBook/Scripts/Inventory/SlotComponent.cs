using UnityEngine;

public class SlotComponent : MonoBehaviour
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
}
