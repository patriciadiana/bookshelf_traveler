using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : SingletonPersistent<InputManager> 
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;

    private TouchControl touchControl;

    private void Awake()
    {
        base.Awake();
        touchControl = new TouchControl();
    }

    private void OnEnable()
    {
        if (touchControl != null)
            touchControl.Enable();
    }

    private void OnDisable()
    {
        if (touchControl != null)
            touchControl.Disable();
    }

    private void Start()
    {
        /*Subscribe to an event (starting to touch) and pass the information from that event*/
        touchControl.Touch.TouchPress.started += context => StartTouch(context);
        touchControl.Touch.TouchPress.canceled += context => EndTouch(context);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(touchControl.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    private void EndTouch(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(touchControl.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }
}