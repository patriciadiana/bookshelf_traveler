using System.Collections;
using UnityEngine;

public class SuspectInteraction : MonoBehaviour, IInteractable
{
    public DialogueComponent dialogue;
    public NPCDialogue readText;

    public bool isGuilty;

    private bool alreadyInteracted = false;

    public bool CanInteract()
    {
        return dialogue != null && !dialogue.isDialogueActive;
    }

    public void Interact()
    {
        if (SuspectsManager.Instance.IsAccusationPhase())
        {
            SuspectsManager.Instance.MakeAccusation(this);
            return;
        }

        if (dialogue == null) return;

        dialogue.dialogueData = readText;
        DialogueSystem.Instance.HandleInteraction(dialogue);

        if (!alreadyInteracted)
        {
            StartCoroutine(RegisterAfterDialogue());
        }
    }

    private IEnumerator RegisterAfterDialogue()
    {
        yield return new WaitUntil(() => dialogue.isDialogueActive);

        yield return new WaitUntil(() => !dialogue.isDialogueActive);

        alreadyInteracted = true;
        SuspectsManager.Instance.RegisterInteraction();
    }
}
