using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPlaceWall : ILevelEditorAction
{
    ReticleController reticle;
    Vector3 reticlePosition;

    GameWallAnchor wallAnchor;

    GameWallAnchorPool anchorPool;
    
    public ActionPlaceWall(ReticleController reticle, GameWallAnchor anchor, GameWallAnchorPool pool)
    {
        this.reticle = reticle;
        reticlePosition = reticle.transform.position;

        wallAnchor = anchor;
        anchorPool = pool; 
    }

    public void RedoAction()
    {
        wallAnchor = anchorPool.GetObject();
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
