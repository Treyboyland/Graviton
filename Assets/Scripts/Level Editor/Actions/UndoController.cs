using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

/// <summary>
/// Controls both undo and redo operations for the level editor
/// </summary>
public class UndoController : MonoBehaviour
{
    [SerializeField]
    LevelEditorSaveButton saveButton;

    /// <summary>
    /// A stack of undo actions. The UndoAction method should be called with these
    /// </summary>
    /// <typeparam name="ILevelEditorAction"></typeparam>
    /// <returns></returns>
    Stack<ILevelEditorAction> undoActions = new Stack<ILevelEditorAction>();

    /// <summary>
    /// A stack of redo actions. The RedoAction method should be called with these
    /// </summary>
    Stack<ILevelEditorAction> redoActions = new Stack<ILevelEditorAction>();

    /// <summary>
    /// True if the user is allowed to undo actions
    bool allowUndo = true;

    /// <summary>
    /// True if the user is allowed to undo actions
    /// </summary>
    /// <value></value>
    public bool AllowUndo
    {
        get
        {
            return allowUndo;
        }
        set
        {
            allowUndo = value;
        }
    }

    bool wasLevelSaved = false;

    public bool WasLevelSaved
    {
        get
        {
            return wasLevelSaved;
        }
        set
        {
            wasLevelSaved = value;
        }
    }

    /// <summary>
    /// True if the undo stack is empty
    /// </summary>
    /// <value></value>
    public bool IsUndoEmpty
    {
        get
        {
            return undoActions.Count == 0;
        }
    }

    public class Events
    {
        [Serializable]
        public class ActionDone : UnityEvent<ILevelEditorAction> { }
    }

    /// <summary>
    /// Should be invoked whenever the user completes an action in the editor
    /// </summary>
    public Events.ActionDone OnActionDone;

    // Start is called before the first frame update
    void Start()
    {
        saveButton.OnLevelCreated.AddListener((unused) => wasLevelSaved = true);
        OnActionDone.AddListener(action =>
        {
            wasLevelSaved = false;
            redoActions.Clear();
            undoActions.Push(action);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (allowUndo)
        {
            HandleUndo();
        }
    }

    void HandleUndo()
    {
        if (Input.GetButtonDown("Undo"))
        {
            Undo();
        }
        else if (Input.GetButtonDown("Redo"))
        {
            Redo();
        }
    }

    /// <summary>
    /// Should be hooked to event
    /// </summary>
    public void TryUndo()
    {
        if (allowUndo)
        {
            Undo();
        }
    }

    /// <summary>
    /// Should be hooked to event
    /// </summary>
    public void TryRedo()
    {
        if (allowUndo)
        {
            Redo();
        }
    }



    /// <summary>
    /// Undo an action, and append it to the redo queue
    /// </summary>
    void Undo()
    {
        if (undoActions.Count != 0)
        {
            var action = undoActions.Pop();
            action.UndoAction();
            redoActions.Push(action);
            wasLevelSaved = false;
        }
    }

    /// <summary>
    /// Redo an undone action
    /// </summary>
    void Redo()
    {
        if (redoActions.Count != 0)
        {
            var action = redoActions.Pop();
            action.RedoAction();
            undoActions.Push(action);
            wasLevelSaved = false;
        }
    }
}
