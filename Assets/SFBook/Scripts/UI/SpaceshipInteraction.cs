using System.Collections;
using UnityEngine;

public class SpaceshipInteraction : MonoBehaviour,IInteractable
{
    public DialogueComponent dialogue;
    public NPCDialogue readText;

    private void Start()
    {
        SoundManager.PlayMusic(MusicType.SF_AMBIENT);
    }

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
