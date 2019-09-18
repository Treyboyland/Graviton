using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action created when the user deletes a wall in the editor
/// </summary>
public class ActionDeleteWall : ILevelEditorAction
{
    /// <summary>
    /// List of anchors deleted
    /// </summary>
    List<GameWallAnchor> anchors;
    
    /// <summary>
    /// List of data of walls at deletion. INVARIANT: anchors and wallData indices should match
    /// </summary>
    List<WallAnchorData> wallData;

    /// <summary>
    /// Creates a wall deletion action
    /// </summary>
    /// <param name="anchors">Anchors for the walls deleted</param>
    public ActionDeleteWall(List<GameWallAnchor> anchors)
    {
        this.anchors = anchors;
        wallData = new List<WallAnchorData>(anchors.Count);
        foreach (GameWallAnchor anchor in anchors)
        {
            WallAnchorData data;
            data.LocalScale = anchor.LocalScale;
            data.Position = anchor.transform.position;
            wallData.Add(data);
        }
    }

    /// <summary>
    /// Restores the walls
    /// </summary>
    public void UndoAction()
    {
        for (int i = 0; i < anchors.Count; i++)
        {
            //NOTE: Because anchors can be reused (e.g. delete, then set, then undo set and delete), we need to set their data again
            anchors[i].gameObject.SetActive(true);
            anchors[i].transform.position = wallData[i].Position;
            anchors[i].LocalScale = wallData[i].LocalScale;
        }
    }

    /// <summary>
    /// Deletes the walls
    /// </summary>
    public void RedoAction()
    {
        foreach (GameWallAnchor anchor in anchors)
        {
            anchor.gameObject.SetActive(false);
        }
    }
}
