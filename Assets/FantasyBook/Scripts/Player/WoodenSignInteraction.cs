using NUnit.Framework.Internal.Execution;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WoodenSignInteraction : MonoBehaviour, IInteractable
{
    private InventoryComponent playerInventory;
    private DialogueComponent dialogue;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<InventoryComponent>();
        dialogue = GetComponent<DialogueComponent>();
    }
    public bool CanInteract()
    {
        return dialogue != null && !DialogueSystem.Instance.IsDialogueActive();
    }

    public void Interact()
    {
        if (dialogue == null || dialogue.dialogueData == null)
            return;

        if (playerInventory != null)
        {
            ItemData swordItem = playerInventory.items
                .Find(item => item.itemId == "sword");

            if (swordItem != null)
            {
                SceneManager.LoadScene("_2DragonBattle");
                SoundManager.PlayMusic(MusicType.DRAGON_BATTLE);
                return;
            }
            else
            {
                DialogueSystem.Instance.HandleInteraction(dialogue);
            }
        }
    }
}
