using UnityEngine;

public static class PanelEvents
{
    public static System.Action<GameObject, string> OnOpenPanel;
    public static System.Action OnClosePanel;
}

public static class InteractionState
{
    public static bool IsUIOpen;
}

public class InteractableObjectWithPanel : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject panelToShow;
    [SerializeField] private string panelMessage;
    public DialogueComponent dialogue;
    public NPCDialogue readText;

    public bool CanInteract()
    {
        return dialogue != null
            && !dialogue.isDialogueActive
            && !InteractionState.IsUIOpen;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        InteractionState.IsUIOpen = true;

        dialogue.dialogueData = readText;
        DialogueSystem.Instance.HandleInteraction(dialogue);

        if (panelToShow != null)
        {
            PanelEvents.OnOpenPanel?.Invoke(panelToShow, panelMessage);
        }
    }
}
