using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    [SerializeField] Transform targetPosition;
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D newSceneCollider;

    private bool playerInRange = false;

    public bool CanInteract()
    {
        return playerInRange && EvidencesInteraction.crimeSceneInvestigated;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        SceneTransitions.Instance.StartTransition(player, targetPosition, newSceneCollider);
        EvidencesInteraction.crimeSceneInvestigated = false;
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