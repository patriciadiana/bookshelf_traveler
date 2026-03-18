using System.Collections;
using TMPro;
using UnityEngine;

public class MissingClueInteraction : MonoBehaviour
{
    [SerializeField] PolygonCollider2D newSceneCollider;
    [SerializeField] GameObject player;
    [SerializeField] BackgroundTouchDetector backgroundTouchDetector;

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

        if (backgroundTouchDetector != null)
        {
            backgroundTouchDetector.enabled = true;
        }

        SceneTransitions.Instance.StartTransitionWithoutPlayer(player, newSceneCollider);
    }
}
