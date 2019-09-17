using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionToggleWallDamagingState : ILevelEditorAction
{
    List<GameWall> walls;

    public ActionToggleWallDamagingState(List<GameWall> walls)
    {
        this.walls = walls;
    }

    public void RedoAction()
    {
        foreach (GameWall wall in walls)
        {
            wall.IsDamaging = !wall.IsDamaging;
        }
    }


    public void UndoAction()
    {
        foreach (GameWall wall in walls)
        {
            wall.IsDamaging = !wall.IsDamaging;
        }
    }
}
