using UnityEngine;

public class ReadableObject : MonoBehaviour, IInteractable
{
    public DialogueComponent dialogue;
    public NPCDialogue readText;

    public bool CanInteract()
    {
        return dialogue != null && !dialogue.isDialogueActive;
    }

    public void Interact()
    {
        if (dialogue == null) return;

        dialogue.dialogueData = readText;

        if (!EvidencesInteraction.crimeSceneInvestigated)
        {
            DialogueSystem.Instance.HandleInteraction(dialogue);
        }
    }
}
