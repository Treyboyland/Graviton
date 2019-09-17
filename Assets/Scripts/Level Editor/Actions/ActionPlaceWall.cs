using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlaceWall : ILevelEditorAction
{
    ReticleController reticle;
    Vector3 reticlePosition;

    GameWallAnchor wallAnchor;
    
    public ActionPlaceWall(ReticleController reticle, GameWallAnchor anchor)
    {
        this.reticle = reticle;
        reticlePosition = reticle.transform.position;
        wallAnchor = anchor;
    }

    public void RedoAction()
    {
        reticle.transform.position = reticlePosition;
        wallAnchor.transform.position = reticlePosition;
        wallAnchor.ShouldScale = true;
        wallAnchor.ShouldTrack = false;
        wallAnchor.gameObject.SetActive(true);
        reticle.OnFlicker.Invoke();
    }

    public void UndoAction()
    {
        wallAnchor.gameObject.SetActive(false);
        reticle.OnStopFlickering.Invoke();
        reticle.transform.position = reticlePosition;
    }
}
