using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    GameEventGeneric<Vector2> onMove;

    [SerializeField]
    GameEvent onUndo;

    [SerializeField]
    GameEvent onRedo;

    [SerializeField]
    GameEvent onScreenshot;

    [SerializeField]
    GameEvent onSpeedUp;

    [SerializeField]
    GameEvent onSlowDown;

    [SerializeField]
    GameEvent onPause;

    [SerializeField]
    GameEvent onSubmit;

    [SerializeField]
    GameEvent onQuit;

    [SerializeField]
    GameEvent onReset;

    [SerializeField]
    GameEvent onAnyKey;


    public void HandleMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    public void HandleUndo(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onUndo.Invoke();
        }
    }

    public void HandleRedo(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onRedo.Invoke();
        }
    }

    public void HandleScreenshot(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onScreenshot.Invoke();
        }
    }

    public void HandleSpeedUp(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onSpeedUp.Invoke();
        }
    }

    public void HandleSlowDown(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onSlowDown.Invoke();
        }
    }

    public void HandlePause(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onPause.Invoke();
        }
    }

    public void HandleSubmit(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onSubmit.Invoke();
        }
    }

    public void HandleQuit(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onQuit.Invoke();
        }
    }

    public void HandleReset(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onReset.Invoke();
        }
    }

    public void HandleAnyKey(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onAnyKey.Invoke();
        }
    }
}
