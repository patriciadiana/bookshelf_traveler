using UnityEngine;

public class EvidencesInteraction : MonoBehaviour
{
    public DialogueComponent dialogue;
    public NPCDialogue suspectText;
    public NPCDialogue weaponText;
    public NPCDialogue crimeSceneText;
    public NPCDialogue suspectsText;
    public NPCDialogue witnessStatementText;

    public static bool crimeSceneInvestigated = false;

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
        crimeSceneInvestigated = true;
    }
    public void SuspectsInteraction()
    {
        dialogue.dialogueData = suspectsText;
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
    public void WitnessStatementInteraction()
    {
        dialogue.dialogueData = witnessStatementText;
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
}
