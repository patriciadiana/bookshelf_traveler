using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "InventoryItem")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public Sprite icon;
}