using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Action that should be created when a player first puts down a wall
/// </summary>
public class ActionPlaceWall : ILevelEditorAction
{
    ReticleController reticle;
    Vector3 reticlePosition;

    GameWallAnchor wallAnchor;

    /// <summary>
    /// Creates an instance of this action
    /// </summary>
    /// <param name="reticle">The editor reticle</param>
    /// <param name="anchor">The wall placed</param>
    public ActionPlaceWall(ReticleController reticle, GameWallAnchor anchor)
    {
        this.reticle = reticle;
        reticlePosition = reticle.transform.position;
        wallAnchor = anchor;
    }

    /// <summary>
    /// Puts the reticle at its position when the wall was created, places the wall at that position,
    /// and sets the wall to track movement
    /// </summary>
    public void RedoAction()
    {
        reticle.transform.position = reticlePosition;
        wallAnchor.transform.position = reticlePosition;
        wallAnchor.ShouldScale = true;
        wallAnchor.ShouldTrack = false;
        wallAnchor.gameObject.SetActive(true);
        reticle.OnFlicker.Invoke();
    }

    /// <summary>
    /// Deactivates the wall, and moves the reticle to the position where the wall was placed
    /// </summary>
    public void UndoAction()
    {
        wallAnchor.gameObject.SetActive(false);
        reticle.OnStopFlickering.Invoke();
        reticle.transform.position = reticlePosition;
    }
}
