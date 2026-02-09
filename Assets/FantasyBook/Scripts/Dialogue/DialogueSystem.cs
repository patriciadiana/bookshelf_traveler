using System.Collections;
using UnityEngine;

public class DialogueSystem : Singleton<DialogueSystem>
{
    private DialogueUIController ui;

    private void Awake()
    {
        ui = DialogueUIController.Instance;
    }

    public void HandleInteraction(DialogueComponent dialogue)
    {
        if (dialogue.isDialogueActive)
            NextLine(dialogue);
        else
            StartDialogue(dialogue);
    }

    void StartDialogue(DialogueComponent dialogue)
    {
        dialogue.isDialogueActive = true;
        dialogue.dialogueIndex = 0;

        ui.SetNPCInfo(
            dialogue.dialogueData.npcName,
            dialogue.dialogueData.npcPortrait
        );

        ui.ShowDialogueUI(true);
        DisplayCurrentLine(dialogue);
    }

    void NextLine(DialogueComponent dialogue)
    {
        if (dialogue.isTyping)
        {
            StopAllCoroutines();
            ui.SetDialogueText(dialogue.dialogueData.dialogueLines[dialogue.dialogueIndex]);
            dialogue.isTyping = false;
        }

        ui.ClearChoices();

        if (dialogue.dialogueData.endDialogueLines.Length > dialogue.dialogueIndex &&
            dialogue.dialogueData.endDialogueLines[dialogue.dialogueIndex])
        {
            EndDialogue(dialogue);
            return;
        }

        foreach (DialogueChoice choice in dialogue.dialogueData.choices)
        {
            if (choice.dialogueIndex == dialogue.dialogueIndex)
            {
                DisplayChoice(dialogue, choice);
                return;
            }
        }

        dialogue.dialogueIndex++;

        if (dialogue.dialogueIndex < dialogue.dialogueData.dialogueLines.Length)
            DisplayCurrentLine(dialogue);
        else
            EndDialogue(dialogue);
    }

    void DisplayCurrentLine(DialogueComponent dialogue)
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine(dialogue));
    }

    IEnumerator TypeLine(DialogueComponent dialogue)
    {
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
            NextLine(dialogue);
        }
    }

    void DisplayChoice(DialogueComponent dialogue, DialogueChoice choice)
    {
        for (int i = 0; i < choice.choices.Length; i++)
        {
            int nextIndex = choice.nextDialogueIndex[i];
            ui.CreateChoiceButton(
                choice.choices[i],
                () => ChooseOption(dialogue, nextIndex)
            );
        }
    }

    void ChooseOption(DialogueComponent dialogue, int nextIndex)
    {
        dialogue.dialogueIndex = nextIndex;
        ui.ClearChoices();
        DisplayCurrentLine(dialogue);
    }

    void EndDialogue(DialogueComponent dialogue)
    {
        StopAllCoroutines();
        dialogue.isDialogueActive = false;
        ui.SetDialogueText("");
        ui.ShowDialogueUI(false);
    }
}
