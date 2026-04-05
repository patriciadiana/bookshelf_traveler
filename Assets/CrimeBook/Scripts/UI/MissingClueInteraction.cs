using System.Collections;
using UnityEngine;

public class MissingClueInteraction : MonoBehaviour
{
    [SerializeField] PolygonCollider2D newSceneCollider;
    [SerializeField] GameObject player;

    public DialogueComponent dialogue;
    public NPCDialogue missingClueText;

    public void MissingClue()
    {
        dialogue.dialogueData = missingClueText;
        DialogueSystem.Instance.HandleInteraction(dialogue);

        StartCoroutine(WaitForDialogue());
    }

    IEnumerator WaitForDialogue()
    {
        yield return new WaitWhile(() => dialogue.isDialogueActive);

        gameObject.SetActive(false);

        SaveStateManager stateManager = FindFirstObjectByType<SaveStateManager>();
        if (stateManager != null)
        {
            stateManager.isInSuspectMode = true;
        }

        SceneTransitions.Instance.StartTransitionWithoutPlayer(player, newSceneCollider);
    }
}