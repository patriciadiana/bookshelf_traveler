using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteractable
{
    [SerializeField] Transform targetPosition;
    [SerializeField] GameObject player;
    [SerializeField] PolygonCollider2D newSceneCollider;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (!EvidencesInteraction.crimeSceneInvestigated)
        {
            return;
        }

        SceneTransitions.Instance.StartTransition(player, targetPosition, newSceneCollider);
        EvidencesInteraction.crimeSceneInvestigated = false;
    }
}
