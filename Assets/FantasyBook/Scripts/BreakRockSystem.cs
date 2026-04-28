using UnityEngine;

public class BreakRockSystem : MonoBehaviour
{
    public void Interact(GameObject entity)
    {
        RockComponent rock = entity.GetComponent<RockComponent>();
        if (rock == null || rock.isBroken)
            return;

        rock.hits--;

        SoundManager.PlaySound(SoundType.F_HITTING_ROCK);

        if (rock.hits > 0)
            return;

        rock.isBroken = true;

        SpriteRenderer renderer = entity.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sprite = rock.brokenSprite;
        }

        SoundManager.PlaySound(SoundType.F_ITEM_EQUIP);
        InventorySystem.AddItem(rock.playerInventory, rock.rockItem);

        Destroy(entity);
    }
}
