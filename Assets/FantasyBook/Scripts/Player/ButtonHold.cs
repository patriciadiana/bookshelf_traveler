using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { Left, Right, Jump }

    [SerializeField] private ButtonType buttonType;
    [SerializeField] private PlayerSideScrollerMovement player;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (buttonType == ButtonType.Left)
            player.MoveLeft();
        else if (buttonType == ButtonType.Right)
            player.MoveRight();
        else if (buttonType == ButtonType.Jump)
            player.Jump();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.StopMoving();
    }
}
