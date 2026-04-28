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
        if (dialogue == null || dialogue.isDialogueActive)
            return;

        InventoryComponent playerInventory = FindFirstObjectByType<InventoryComponent>();

        if (!hasSpokenOnce)
        {
            hasSpokenOnce = true;
        }
        else if (playerInventory != null && playerInventory.items.Exists(item => item.itemId == "diamond"))
        {
            ItemData diamondItem = playerInventory.items.Find(item => item.itemId == "diamond");
            playerInventory.items.Remove(diamondItem);

            dialogue.dialogueData = rewardDialogue;

            StartCoroutine(GiveSwordAfterDialogue(playerInventory));
        }
        else
        {
            dialogue.dialogueData = defaultDialogue;
        }

        DialogueSystem.Instance.HandleInteraction(dialogue);
    }

    private IEnumerator GiveSwordAfterDialogue(InventoryComponent playerInventory)
    {
        while (dialogue.isDialogueActive)
        {
            yield return null;
        }

        SoundManager.PlaySound(SoundType.F_ITEM_EQUIP);
        InventorySystem.AddItem(playerInventory, swordItem);
    }


    public void Close()
    {
        DialogueSystem.Instance.EndDialogue();
    }
}
