using UnityEngine;

public class RockInteraction : MonoBehaviour, IInteractable
{
    private RockComponent rock;
    private BreakRockSystem breakSystem;

    private void Awake()
    {
        rock = GetComponent<RockComponent>();
        breakSystem = FindFirstObjectByType<BreakRockSystem>();
    }

    public bool CanInteract()
    {
        return rock != null && !rock.isBroken;
    }

    public void Interact()
    {
        breakSystem.Interact(gameObject);
    }
}
