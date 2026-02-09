using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour, IInteractable
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
        if (dialogue == null || dialogue.dialogueData == null)
            return;

        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
}
