using UnityEngine;

public static class PanelEvents
{
    public static System.Action<GameObject, string> OnOpenPanel;
    public static System.Action OnClosePanel;
}

public class InteractableObjectWithPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject panelToShow;
    [SerializeField] private string panelMessage;
    public DialogueComponent dialogue;
    public NPCDialogue readText;

    public bool CanInteract()
    {
        return dialogue != null && !dialogue.isDialogueActive;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        dialogue.dialogueData = readText;
        DialogueSystem.Instance.HandleInteraction(dialogue);

        if (panelToShow != null)
        {
            PanelEvents.OnOpenPanel?.Invoke(panelToShow, panelMessage);
        }
    }
}
