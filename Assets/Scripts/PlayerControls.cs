using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
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
    GameEventGeneric<bool> onSpeedUp;

    [SerializeField]
    GameEventGeneric<bool> onSlowDown;

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

    [SerializeField]
    GameEvent onChangeSymmetry;

    [SerializeField]
    GameEvent onTestSpawns;

    [SerializeField]
    GameEvent onDelete;




    public void HandleMove(InputAction.CallbackContext context)
    {
        // if (context.performed)
        // {
        onMove.Invoke(context.ReadValue<Vector2>());
        // }
    }

    public void HandleChangeSymmetry(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onChangeSymmetry.Invoke();
        }
    }

    public void HandleTestSpawns(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onTestSpawns.Invoke();
        }
    }

    public void HandleDelete(InputAction.CallbackContext context)
    {
        if (context.performed && context.ReadValueAsButton())
        {
            onDelete.Invoke();
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
        onSpeedUp.Invoke(context.ReadValueAsButton());
    }

    public void HandleSlowDown(InputAction.CallbackContext context)
    {
        onSlowDown.Invoke(context.ReadValueAsButton());
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

    public void HandleAnyKeyEvent()
    {
        IdleTimerHelper.ShouldRestartTimer = true;
    }
}
