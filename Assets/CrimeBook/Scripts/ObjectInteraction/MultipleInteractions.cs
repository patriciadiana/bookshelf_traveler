using UnityEngine;

public class MultipleInteractions : MonoBehaviour, IInteractable
{
    private IInteractable[] interactables;
    private bool playerInRange = false;

    private void Awake()
    {
        interactables = GetComponents<IInteractable>();
    }

    public bool CanInteract()
    {
        if (!playerInRange)
            return false;

        foreach (var interactable in interactables)
        {
            if ((Component)interactable == this)
                continue;

            if (interactable.CanInteract())
                return true;
        }

        return false;
    }

    public void Interact()
    {
        if (!playerInRange)
            return;

        foreach (var interactable in interactables)
        {
            if ((Component)interactable == this)
                continue;

            if (interactable.CanInteract())
            {
                interactable.Interact();
            }
        }
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