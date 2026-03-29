using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public GameObject joystick;
    public GameObject joystickBackground;

    public Vector2 joystickVector;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    private void Start()
    {
        joystickOriginalPos = joystickBackground.transform.position;
        joystickRadius = joystickBackground.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = eventData.position;
        joystickVector = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if(joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVector * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVector * joystickRadius;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //joystick.transform.position = eventData.position;
        //joystickBackground.transform.position = eventData.position;
        joystickTouchPos = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickVector = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        //joystickBackground.transform.position = joystickOriginalPos;
    }
}
