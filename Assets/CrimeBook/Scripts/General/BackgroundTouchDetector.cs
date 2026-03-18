using UnityEngine;

public class BackgroundTouchDetector : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStartTouch += HandleTouch;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStartTouch -= HandleTouch;
        }
    }

    private void HandleTouch(Vector2 screenPosition, float time)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);

        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 0f, interactableLayer);

        if (hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null && interactable.CanInteract())
            {
                interactable.Interact();
            }
        }
    }
}
