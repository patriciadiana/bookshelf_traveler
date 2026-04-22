using System.Collections;
using UnityEngine;

public class EnemiesIncomingInteraction : MonoBehaviour, IInteractable
{
    public Transform newCameraPos;
    public DialogueComponent dialogue;
    public NPCDialogue readText;
    public NPCDialogue afterBattleText;

    public GameObject UIControls;
    public GameObject waveSpawner;

    public SFSaveLoadData saveData;

    private void Start()
    {
        saveData = FindFirstObjectByType<SFSaveLoadData>();

        UIControls.SetActive(false);
        waveSpawner.SetActive(false);
    }

    public bool CanInteract()
    {
        return dialogue != null && !dialogue.isDialogueActive;
    }

    public void Interact()
    {
        if (dialogue == null) return;

        dialogue.dialogueData = readText;

        if (saveData != null && saveData.enteredBattleMode)
        {
            dialogue.dialogueData = afterBattleText;
        }

        DialogueSystem.Instance.HandleInteraction(dialogue);

        StartCoroutine(WaitForDialogue());
    }

    IEnumerator WaitForDialogue()
    {
        yield return new WaitWhile(() => dialogue.isDialogueActive);

        yield return StartCoroutine(
            SceneTransitions.Instance.StartCameraTransition(newCameraPos)
        );

        UIControls.SetActive(true);
        waveSpawner.SetActive(true);

        if (saveData != null)
            saveData.enteredBattleMode = true;

        gameObject.SetActive(false);
    }
}
