using UnityEngine;

public class RockComponent : MonoBehaviour
{
    public int hitsRequired = 5;
    public Sprite brokenSprite;

    public InventoryComponent playerInventory;
    public ItemData rockItem;

    [HideInInspector] public int hits;
    [HideInInspector] public bool isBroken;

    private void Awake()
    {
        hits = hitsRequired;
    }
}
