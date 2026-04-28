using UnityEngine;

public class NormalNPCDialogue : MonoBehaviour, IInteractable
{
    private DialogueComponent dialogue;

    private void Awake()
    {
        dialogue = GetComponent<DialogueComponent>();
    }

    public bool CanInteract()
    {
        return dialogue != null && !dialogue.isDialogueActive;
    }

    public void Interact()
    {
        if (dialogue == null || dialogue.isDialogueActive)
            return;

        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
}
