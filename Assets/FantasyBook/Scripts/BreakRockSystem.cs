using UnityEngine;

public class BreakRockSystem : MonoBehaviour
{
    public void Interact(GameObject entity)
    {
        RockComponent rock = entity.GetComponent<RockComponent>();
        if (rock == null || rock.isBroken)
            return;

        rock.hits--;

        if (rock.hits <= 0)
        {
            rock.isBroken = true;
            entity.GetComponent<SpriteRenderer>().sprite = rock.brokenSprite;

            InventorySystem.AddItem(rock.playerInventory, rock.rockItem);
        }
    }
}
