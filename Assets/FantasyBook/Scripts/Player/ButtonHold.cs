using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Jump }

    [SerializeField] private ButtonType buttonType;
    [SerializeField] private PlayerSideScrollerMovement player;

    private static bool isLeftPressed = false;
    private static bool isRightPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                isLeftPressed = true;
                player.MoveLeft();
                break;
            case ButtonType.Right:
                isRightPressed = true;
                player.MoveRight();
                break;
            case ButtonType.Jump:
                player.Jump();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                isLeftPressed = false;
                UpdateMovement();
                break;
            case ButtonType.Right:
                isRightPressed = false;
                UpdateMovement();
                break;
            case ButtonType.Jump:

                break;
        }
    }

    private void UpdateMovement()
    {
        if (isLeftPressed)
            player.MoveLeft();
        else if (isRightPressed)
            player.MoveRight();
        else
            player.StopMoving();
    }
}