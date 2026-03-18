using UnityEngine;
using UnityEngine.TextCore.Text;

public class SideScrollTouchInput : MonoBehaviour
{
    [SerializeField] private SidescrollPlayerMovement moveCharacter;

    private void OnEnable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStartTouch += HandleTouch;
        }
    }

    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnStartTouch -= HandleTouch;
        }
    }

    private void HandleTouch(Vector2 screenPosition, float time)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0f;

        moveCharacter.SetTargetX(worldPosition.x);
    }
}
