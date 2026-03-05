using UnityEngine;

public class MultipleInteractions : MonoBehaviour, IInteractable
{
    private IInteractable[] interactables;

    private void Awake()
    {
        interactables = GetComponents<IInteractable>();
    }

    public bool CanInteract()
    {
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
        foreach (var interactable in interactables)
        {
            if ((Component)interactable == this)
                continue;

            if (interactable.CanInteract())
                interactable.Interact();
        }
    }
}