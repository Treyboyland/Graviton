using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionToggleWallDamagingState : ILevelEditorAction
{
    GameWallAnchor anchor;

    public ActionToggleWallDamagingState(GameWallAnchor anchor)
    {
        this.anchor = anchor;
    }

    public void RedoAction()
    {
        anchor.GameWall.IsDamaging = !anchor.GameWall.IsDamaging;
    }


    public void UndoAction()
    {
        anchor.GameWall.IsDamaging = !anchor.GameWall.IsDamaging;
    }
}
