using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDeleteWall : ILevelEditorAction
{
    struct WallData
    {
        public Vector3 Scale;
        public Vector3 Position;
    }
    List<GameWallAnchor> anchors;
    List<WallData> wallData;

    public ActionDeleteWall(List<GameWallAnchor> anchors)
    {
        this.anchors = anchors;
        wallData = new List<WallData>(anchors.Count);
        foreach (GameWallAnchor anchor in anchors)
        {
            WallData data;
            data.Scale = anchor.LocalScale;
            data.Position = anchor.transform.position;
            wallData.Add(data);
        }
    }

    public void UndoAction()
    {
        for (int i = 0; i < anchors.Count; i++)
        {
            //NOTE: Because anchors can be reused (e.g. delete, then set, then undo set and delete), we need to set their data again
            anchors[i].gameObject.SetActive(true);
            anchors[i].transform.position = wallData[i].Position;
            anchors[i].LocalScale = wallData[i].Scale;
        }
    }

    public void RedoAction()
    {
        foreach (GameWallAnchor anchor in anchors)
        {
            anchor.gameObject.SetActive(false);
        }
    }
}
