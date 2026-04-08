using System.Collections;
using TMPro;
using UnityEngine;

public class EnemiesIncomingInteraction : MonoBehaviour, IInteractable
{
    public Transform newCameraPos;
    public DialogueComponent dialogue;
    public NPCDialogue readText;

    public GameObject UIControls;
    public GameObject waveSpawner;

    private void Start()
    {
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
        gameObject.SetActive(false);
    }
}
