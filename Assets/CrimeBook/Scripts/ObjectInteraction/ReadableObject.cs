using UnityEngine;

public class ReadableObject : MonoBehaviour, IInteractable
{
    public DialogueComponent dialogue;
    public NPCDialogue readText;

    private bool playerInRange = false;

    public bool CanInteract()
    {
        return playerInRange && dialogue != null && !dialogue.isDialogueActive;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        dialogue.dialogueData = readText;

        if (!EvidencesInteraction.crimeSceneInvestigated)
        {
            DialogueSystem.Instance.HandleInteraction(dialogue);
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