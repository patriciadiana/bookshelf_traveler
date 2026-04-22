using System.Collections;
using UnityEngine;

public class TurnLampOn : MonoBehaviour, IInteractable
{
    public GameObject lightSprite;

    private bool isOn = false;
    private bool canInteract = true;
    private bool playerInRange = false;

    private void Awake()
    {
        if (lightSprite != null)
            lightSprite.SetActive(false);
    }

    public bool CanInteract()
    {
        return playerInRange && canInteract;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        isOn = !isOn;

        if (lightSprite != null)
            lightSprite.SetActive(isOn);

        StartCoroutine(ResetInteract());
    }

    private IEnumerator ResetInteract()
    {
        canInteract = false;
        yield return new WaitForSeconds(0.1f);
        canInteract = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}