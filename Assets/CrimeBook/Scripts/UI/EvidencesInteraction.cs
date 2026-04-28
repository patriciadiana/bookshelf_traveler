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

    [SerializeField] private SoundType interactSound;

    public void SuspectInteraction()
    {
        dialogue.dialogueData = suspectText;
        SoundManager.PlaySound(interactSound);
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }

    public void WeaponInteraction()
    {
        dialogue.dialogueData = weaponText;
        SoundManager.PlaySound(interactSound);
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }

    public void CrimeSceneInteraction()
    {
        dialogue.dialogueData = crimeSceneText;
        SoundManager.PlaySound(interactSound);
        DialogueSystem.Instance.HandleInteraction(dialogue);
        crimeSceneInvestigated = true;
    }
    public void SuspectsInteraction()
    {
        dialogue.dialogueData = suspectsText;
        SoundManager.PlaySound(interactSound);
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
    public void WitnessStatementInteraction()
    {
        dialogue.dialogueData = witnessStatementText;
        SoundManager.PlaySound(interactSound);
        DialogueSystem.Instance.HandleInteraction(dialogue);
    }
}
