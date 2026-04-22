using System.Collections;
using UnityEngine;

public class SpaceshipInteraction : MonoBehaviour,IInteractable
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
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
}
