using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    private DialogueComponent dialogue;

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
        if (dialogue == null || dialogue.dialogueData == null)
            return;

        InventoryComponent playerInventory = FindFirstObjectByType<InventoryComponent>();

        if (playerInventory != null)
        {
            ItemData diamondItem = playerInventory.items
                .Find(item => item.itemId == "diamond");

            if (diamondItem != null)
            {
                playerInventory.items.Remove(diamondItem);

                SceneManager.LoadScene("_2DragonBattle");
                return;
            }
        }

        DialogueSystem.Instance.HandleInteraction(dialogue);
    }

    public void Close()
    {
        DialogueSystem.Instance.EndDialogue(dialogue);
    }
}
