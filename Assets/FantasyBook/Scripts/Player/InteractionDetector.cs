using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class InteractionDetector : MonoBehaviour
{
    private List<IInteractable> interactablesInRange = new List<IInteractable>();
    public GameObject interactionIcon;
    public LayerMask interactableLayers;
    private UIPanelPopup UIpopup;

    private Camera mainCamera;

    private void Start()
    {
        interactionIcon.SetActive(false);
        mainCamera = Camera.main;
        UIpopup = FindFirstObjectByType<UIPanelPopup>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (UIpopup != null && UIpopup.IsOpen())
            return;

        Vector2 screenPos;

        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else
        {
            return;
        }

        Vector2 worldPos = mainCamera.ScreenToWorldPoint(screenPos);

        Collider2D hit = Physics2D.OverlapPoint(worldPos, interactableLayers);

        if (hit == null)
        {
            return;
        }

        var interactables = hit.GetComponents<IInteractable>();

        foreach (var interactable in interactables)
        {
            if (interactablesInRange.Contains(interactable) && interactable.CanInteract())
            {
                interactable.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactables = collision.GetComponents<IInteractable>();

        foreach (var interactable in interactables)
        {
            if (interactable.CanInteract())
            {
                interactablesInRange.Add(interactable);

                var outline = collision.GetComponent<OutlineController>();
                if (outline != null)
                    outline.EnableOutline();

                interactionIcon.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactables = collision.GetComponents<IInteractable>();

        foreach (var interactable in interactables)
        {
            if (interactablesInRange.Contains(interactable))
            {
                interactablesInRange.Remove(interactable);
            }
        }

        var outline = collision.GetComponent<OutlineController>();
        if (outline != null)
            outline.DisableOutline();

        if (interactablesInRange.Count == 0)
            interactionIcon.SetActive(false);
    }
}