using UnityEngine;

public class BreakableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private int hitsRequired = 5;
    [SerializeField] private Sprite brokenSprite;
    [SerializeField] private GameObject itemUIPrefab;

    private int hits;
    private SpriteRenderer spriteRenderer;
    private bool isBroken;

    private void Awake()
    {
        hits = hitsRequired;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CanInteract()
    {
        return !isBroken;
    }

    public void Interact()
    {
        if (isBroken)
            return;

        hits--;

        if (hits <= 0)
        {
            Break();
        }
    }

    private void Break()
    {
        isBroken = true;
        spriteRenderer.sprite = brokenSprite;

        InventoryItem itemData = gameObject.AddComponent<InventoryItem>();
        itemData.itemUIPrefab = itemUIPrefab;

        gameObject.tag = "Item";
        GetComponent<Collider2D>().isTrigger = true;
    }
}
