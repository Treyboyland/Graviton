using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSetWall : ILevelEditorAction
{
    GameWallAnchor playerWallAnchor;
    Vector3 playerWallAnchorScale;

    GameWallAnchor cpuWallAnchor;
    Vector3 cpuWallAnchorScale;

    ReticleController reticle;
    Vector3 reticlePosition;

    //TODO: THINK ABOUT THIS...WE NEED TO ACCOUNT FOR CPU PLACED WALLS AS WELL. 

    public ActionSetWall(GameWallAnchor playerAnchor, GameWallAnchor cpuAnchor, SymmetricWallPlacer.WallSymmetry symmetry, ReticleController reticle)
    {
        this.reticle = reticle;
        reticlePosition = reticle.transform.position;

        playerWallAnchor = playerAnchor;
        playerWallAnchorScale = playerAnchor.transform.localScale;



        //TODO: Implement
    }


    public void RedoAction()
    {
        playerWallAnchor.ShouldScale = false;
        playerWallAnchor.ShouldTrack = false;
        playerWallAnchor.transform.localScale = playerWallAnchorScale;


        reticle.OnStopFlickering.Invoke();
        reticle.transform.position = reticlePosition;
    }


    public void UndoAction()
    {
        reticle.OnFlicker.Invoke();
        reticle.transform.position = reticlePosition;

        playerWallAnchor.ShouldScale = true;
        cpuWallAnchor.gameObject.SetActive(false);
    }
}
