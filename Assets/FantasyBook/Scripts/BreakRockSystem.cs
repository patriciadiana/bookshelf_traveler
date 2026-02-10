using UnityEngine;

public class BreakRockSystem : MonoBehaviour
{
    public void Interact(GameObject entity)
    {
        RockComponent rock = entity.GetComponent<RockComponent>();
        if (rock == null || rock.isBroken)
            return;

        rock.hits--;

        if (rock.hits > 0)
            return;

        rock.isBroken = true;

        SpriteRenderer renderer = entity.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = rock.brokenSprite;
        }

        InventorySystem.AddItem(rock.playerInventory, rock.rockItem);
    }
}
