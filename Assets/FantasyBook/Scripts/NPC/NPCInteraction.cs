using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    private DialogueComponent dialogue;
    public NPCDialogue rewardDialogue;
    public NPCDialogue defaultDialogue;
    public ItemData swordItem;

    private bool hasSpokenOnce = false;

    private void Awake()
    {
        dialogue = GetComponent<DialogueComponent>();
    }

    public bool CanInteract()
    {
        return dialogue != null && !dialogue.isDialogueActive;
    }

    public void Interact()
    {
        if (dialogue == null)
            return;

        InventoryComponent playerInventory = FindFirstObjectByType<InventoryComponent>();

        if (!hasSpokenOnce)
        {
            DialogueSystem.Instance.HandleInteraction(dialogue);
            hasSpokenOnce = true;
            return;
        }

        if (playerInventory != null)
        {
            ItemData diamondItem = playerInventory.items
                .Find(item => item.itemId == "diamond");

            if (diamondItem != null)
            {
                playerInventory.items.Remove(diamondItem);

                dialogue.dialogueData = rewardDialogue;

                DialogueSystem.Instance.HandleInteraction(dialogue);

                StartCoroutine(GiveSwordAfterDialogue(playerInventory));

                return;
            }
        }

        dialogue.dialogueData = defaultDialogue;

        DialogueSystem.Instance.HandleInteraction(dialogue);
    }

    private IEnumerator GiveSwordAfterDialogue(InventoryComponent playerInventory)
    {
        while (dialogue.isDialogueActive)
        {
            yield return null;
        }

        InventorySystem.AddItem(playerInventory, swordItem);
    }


    public void Close()
    {
        DialogueSystem.Instance.EndDialogue(dialogue);
    }
}
