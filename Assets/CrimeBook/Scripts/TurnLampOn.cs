using System.Collections;
using UnityEngine;

public class TurnLampOn : MonoBehaviour, IInteractable
{
    public Sprite lampOn;
    public Sprite lampOff;

    private SpriteRenderer spriteRenderer;
    private bool isOn = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer != null && lampOff != null)
        {
            spriteRenderer.sprite = lampOff;
        }
    }

    public bool CanInteract()
    {
        return true;
    }

    private bool canInteract = true;

    public void Interact()
    {
        if (!canInteract || spriteRenderer == null) return;

        isOn = !isOn;
        spriteRenderer.sprite = isOn ? lampOn : lampOff;

        StartCoroutine(ResetInteract());
    }

    private IEnumerator ResetInteract()
    {
        canInteract = false;
        yield return new WaitForSeconds(0.1f);
        canInteract = true;
    }
}
