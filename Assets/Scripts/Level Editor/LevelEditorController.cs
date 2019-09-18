using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;

public class LevelEditorController : MonoBehaviour
{
    [SerializeField]
    ReticleController reticle;

    public ReticleController Reticle
    {
        get
        {
            return reticle;
        }
    }

    [SerializeField]
    GameWallAnchorPool wallAnchorPool;

    [SerializeField]
    GameWallHolder wallHolder;

    [SerializeField]
    UndoController undoController;


    bool shouldHandleActions = true;

    public bool ShouldHandleActions
    {
        get
        {
            return shouldHandleActions;
        }
        set
        {
            shouldHandleActions = value;
        }
    }

    public class Events
    {
        [Serializable]
        public class WallPlaced : UnityEvent<GameWallAnchor> { }
    }

    /// <summary>
    /// Invoked when a wall has been placed or 
    /// </summary>
    public Events.WallPlaced OnWallPlaced;

    GameWallAnchor currentAnchor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (shouldHandleActions)
        {
            HandleActions();
        }
    }

    /// <summary>
    /// Returns all game walls with a collider that overlaps the given position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    List<GameWall> GetGameWallsWithPoint(Vector3 position)
    {
        GameObject[] wallObj = GameObject.FindGameObjectsWithTag("GameWall");
        List<GameWall> gameWalls = new List<GameWall>(wallObj.Length);

        for (int i = 0; i < wallObj.Length; i++)
        {
            gameWalls.Add(wallObj[i].GetComponent<GameWall>());
        }

        List<GameWall> toReturn = new List<GameWall>();
        foreach (GameWall wall in gameWalls)
        {
            if (wall.ContainsPoint(position))
            {
                toReturn.Add(wall);
            }
        }

        return toReturn;
    }



    void HandleActions()
    {
        if (Input.GetButtonDown("Submit"))
        {
            //NOTE: I want to be able to toggle damaging 
            if (!reticle.IsFlickering)
            {
                List<GameWall> walls = GetGameWallsWithPoint(reticle.transform.position);
                if (walls.Count == 0)
                {
                    //TODO: Place Wall
                    GameWallAnchor anchor = wallAnchorPool.GetObject();
                    anchor.ShouldScale = true;
                    //anchor.ShouldTrack = false;
                    anchor.transform.position = reticle.transform.position;
                    anchor.LocalScale = new Vector3(0.5f, 0.5f, anchor.LocalScale.z);
                    anchor.Reticle = reticle;
                    anchor.WallHolder = wallHolder;
                    currentAnchor = anchor;
                    currentAnchor.gameObject.SetActive(true);
                    reticle.OnFlicker.Invoke();
                    OnWallPlaced.Invoke(null);

                    ActionPlaceWall actionPlaceWall = new ActionPlaceWall(reticle, anchor);
                    undoController.OnActionDone.Invoke(actionPlaceWall);
                }
                else
                {
                    foreach (GameWall wall in walls)
                    {
                        wall.IsDamaging = !wall.IsDamaging;
                    }
                    ActionToggleWallDamagingState toggleWallDamagingState = new ActionToggleWallDamagingState(walls);
                    undoController.OnActionDone.Invoke(toggleWallDamagingState);
                }
            }
            else
            {
                currentAnchor.ShouldScale = false;
                currentAnchor.ShouldTrack = false;
                reticle.OnStopFlickering.Invoke();
                OnWallPlaced.Invoke(currentAnchor);
            }
        }
        else if (Input.GetButtonDown("Delete") && !reticle.IsFlickering)
        {
            List<GameWallAnchor> deletedAnchors = new List<GameWallAnchor>();
            foreach (GameWall wall in GetGameWallsWithPoint(reticle.transform.position))
            {
                if (wall.IsDeletable && wall.Anchor != null)
                {
                    wall.Anchor.gameObject.SetActive(false);
                    deletedAnchors.Add(wall.Anchor);
                }
            }

            if (deletedAnchors.Count != 0)
            {
                ActionDeleteWall actionDeleteWall = new ActionDeleteWall(deletedAnchors);
                undoController.OnActionDone.Invoke(actionDeleteWall);
            }

        }
    }

}
