using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSetWall : ILevelEditorAction
{
    GameWallAnchor wallAnchor;
    Vector3 wallAnchorScale;

    ReticleController reticle;
    Vector3 reticlePosition;


    public ActionSetWall(GameWallAnchor anchor, ReticleController reticle)
    {
        this.reticle = reticle;
        reticlePosition = reticle.transform.position;

        wallAnchor = anchor;
        wallAnchorScale = anchor.transform.localScale;
        //TODO: Implement
    }


    public void RedoAction()
    {
        wallAnchor.ShouldScale = false;
        wallAnchor.ShouldTrack = false;
        wallAnchor.transform.localScale = wallAnchorScale;


        reticle.OnStopFlickering.Invoke();
        reticle.transform.position = reticlePosition;
    }


    public void UndoAction()
    {
        reticle.OnFlicker.Invoke();
        reticle.transform.position = reticlePosition;

        wallAnchor.ShouldScale = true;
    }
}
