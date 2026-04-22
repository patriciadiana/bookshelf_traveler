using System.Collections;
using UnityEngine;

public class DialogueSystem : Singleton<DialogueSystem>
{
    private DialogueUIController ui;
    private MoveCharacter playerMove;
    private SidescrollPlayerMovement playerMoveSideScroll;

    private DialogueComponent activeDialogue;

    private void Awake()
    {
        ui = DialogueUIController.Instance;
        playerMove = FindFirstObjectByType<MoveCharacter>();
        playerMoveSideScroll = FindFirstObjectByType<SidescrollPlayerMovement>();
    }

    public void HandleInteraction(DialogueComponent dialogue)
    {
        if (activeDialogue != null && activeDialogue != dialogue)
            return;

        if (dialogue.isDialogueActive)
            NextLine();
        else
            StartDialogue(dialogue);
    }

    public void StartDialogue(DialogueComponent dialogue)
    {
        if (activeDialogue != null)
            return;

        activeDialogue = dialogue;

        if (playerMove != null)
            playerMove.SetCanMove(false);

        if (playerMoveSideScroll != null)
            playerMoveSideScroll.SetCanMove(false);

        dialogue.isDialogueActive = true;
        dialogue.dialogueIndex = 0;

        ui.SetNPCInfo(
            dialogue.dialogueData.npcName,
            dialogue.dialogueData.npcPortrait
        );

        ui.ShowDialogueUI(true);
        DisplayCurrentLine();
    }

    void NextLine()
    {
        if (activeDialogue == null)
            return;

        var dialogue = activeDialogue;

        if (dialogue.isTyping)
        {
            StopAllCoroutines();
            ui.SetDialogueText(dialogue.dialogueData.dialogueLines[dialogue.dialogueIndex]);
            dialogue.isTyping = false;
            return;
        }

        ui.ClearChoices();

        if (dialogue.dialogueData.endDialogueLines.Length > dialogue.dialogueIndex &&
            dialogue.dialogueData.endDialogueLines[dialogue.dialogueIndex])
        {
            EndDialogue();
            return;
        }

        foreach (DialogueChoice choice in dialogue.dialogueData.choices)
        {
            if (choice.dialogueIndex == dialogue.dialogueIndex)
            {
                DisplayChoice(choice);
                return;
            }
        }

        dialogue.dialogueIndex++;

        if (dialogue.dialogueIndex < dialogue.dialogueData.dialogueLines.Length)
            DisplayCurrentLine();
        else
            EndDialogue();
    }

    void DisplayCurrentLine()
    {
        if (activeDialogue == null)
            return;

        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        var dialogue = activeDialogue;

        dialogue.isTyping = true;
        ui.SetDialogueText("");

        string line = dialogue.dialogueData.dialogueLines[dialogue.dialogueIndex];

        foreach (char c in line)
        {
            ui.SetDialogueText(ui.dialogueText.text += c);
            yield return new WaitForSeconds(dialogue.dialogueData.typingSpeed);
        }

        dialogue.isTyping = false;

        if (dialogue.dialogueData.autoProgressLines.Length > dialogue.dialogueIndex &&
            dialogue.dialogueData.autoProgressLines[dialogue.dialogueIndex])
        {
            yield return new WaitForSeconds(dialogue.dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoice(DialogueChoice choice)
    {
        var dialogue = activeDialogue;

        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndex[i];

            ui.CreateChoiceButton(
                choice.choices[i],
                () => ChooseOption(nextIndex)
            );
        }
    }

    void ChooseOption(int nextIndex)
    {
        if (activeDialogue == null)
            return;

        activeDialogue.dialogueIndex = nextIndex;
        ui.ClearChoices();
        DisplayCurrentLine();
    }

    public void EndDialogue()
    {
        if (activeDialogue == null)
            return;

        StopAllCoroutines();

        activeDialogue.isDialogueActive = false;
        activeDialogue = null;

        ui.SetDialogueText("");
        ui.ShowDialogueUI(false);

        if (playerMove != null)
            playerMove.SetCanMove(true);

        if (playerMoveSideScroll != null)
            playerMoveSideScroll.SetCanMove(true);
    }

    public bool IsDialogueActive()
    {
        return activeDialogue != null;
    }
}