using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelEditorAction
{
    void RedoAction();
    void UndoAction();
}
