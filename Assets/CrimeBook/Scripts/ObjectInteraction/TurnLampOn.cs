using System.Collections;
using UnityEngine;

public class TurnLampOn : MonoBehaviour, IInteractable
{
    public GameObject lightSprite;

    private bool isOn = false;
    private bool canInteract = true;

    private void Awake()
    {
        if (lightSprite != null)
            lightSprite.SetActive(false);
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (!canInteract) return;

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
}
