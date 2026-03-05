using UnityEngine;

public class EvidencesInteraction : MonoBehaviour
{
    public DialogueComponent dialogue;
    public NPCDialogue suspectText;
    public NPCDialogue weaponText;
    public NPCDialogue crimeSceneText;

    public void SuspectInteraction()
    {
        dialogue.dialogueData = suspectText;
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }

    public void WeaponInteraction()
    {
        dialogue.dialogueData = weaponText;
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }

    public void CrimeSceneInteraction()
    {
        dialogue.dialogueData = crimeSceneText;
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
}
