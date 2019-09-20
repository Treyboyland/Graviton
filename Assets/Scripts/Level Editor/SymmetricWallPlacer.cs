using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

/// <summary>
/// Controls placement of wall in order to maintain level symmetry, if symmetry is active
/// </summary>
public class SymmetricWallPlacer : MonoBehaviour
{

    /// <summary>
    /// Events that the wall placer can fire
    /// </summary>
    public class Events
    {
        /// <summary>
        /// Event that should fire when the symmetry setting is changed
        /// </summary>
        [Serializable]
        public class SettingChanged : UnityEvent<WallSymmetry> { }
    }

    /// <summary>
    /// We listen for the OnPlacedEvent to set the other walls
    /// </summary>
    [SerializeField]
    LevelEditorController controller;

    /// <summary>
    /// We add the placement action to this
    /// </summary>
    [SerializeField]
    UndoController undoController;

    /// <summary>
    /// Instantiates new anchors
    /// </summary>
    [SerializeField]
    GameWallAnchorPool anchorPool;

    /// <summary>
    /// Holder for the walls in the editor
    /// </summary>
    [SerializeField]
    GameWallHolder wallHolder;

    /// <summary>
    /// True if the symmetry value is allowed to change
    /// </summary>
    bool shouldChange = true;

    /// <summary>
    /// True if the symmetry value is allowed to change
    /// </summary>
    /// <value></value>
    public bool ShouldChange
    {
        get
        {
            return shouldChange;
        }
        set
        {
            shouldChange = value;
        }
    }


    /// <summary>
    /// Different types of symmetry available for the editor
    /// </summary>
    public enum WallSymmetry
    {
        /// <summary>
        /// No symmetry is active
        /// </summary>
        NONE,
        /// <summary>
        /// Walls will be placed across the vertical axis (flips X positon and scale)
        /// </summary>
        VERTICAL,
        /// <summary>
        /// Walls wlll be placed across the horizontal axis (flips Y position and scale)
        /// </summary>
        HORIZONTAL,
        /// <summary>
        /// Walls will be placed such that 180 degree rotational symmetry is maintained (flips X and Y position and scale)
        /// </summary>
        ROTATIONAL,
        /// <summary>
        /// Walls will be placed such that vertical, horizontal, and rotational symmetry is maintained
        /// </summary>
        PERFECT,
    }

    /// <summary>
    /// Current symmetry for the editor
    /// </summary>
    [SerializeField]
    WallSymmetry wallSymmetry;

    /// <summary>
    /// Current symmetry for the editor
    /// </summary>
    /// <value></value>
    public WallSymmetry Symmetry
    {
        get
        {
            return wallSymmetry;
        }
        set
        {
            wallSymmetry = value;
            OnSettingChanged.Invoke(wallSymmetry);
        }
    }

    /// <summary>
    /// Method invoked whenever the symmetry setting for the editor has changed
    /// </summary>
    public Events.SettingChanged OnSettingChanged;

    // Start is called before the first frame update
    void Start()
    {
        controller.OnWallPlaced.AddListener((anchor) =>
        {
            if (anchor != null)
            {
                List<GameWallAnchor> anchors;
                switch (wallSymmetry)
                {
                    case WallSymmetry.HORIZONTAL:
                        anchors = HorizontalSpawn(anchor);
                        break;
                    case WallSymmetry.ROTATIONAL:
                        anchors = RotationalSpawn(anchor);
                        break;
                    case WallSymmetry.VERTICAL:
                        anchors = VerticalSpawn(anchor);
                        break;
                    case WallSymmetry.PERFECT:
                        anchors = PerfectSpawn(anchor);
                        break;
                    case WallSymmetry.NONE:
                    default:
                        anchors = new List<GameWallAnchor>();
                        break;
                }
                ActionSetWall actionSetWall = new ActionSetWall(anchor, anchors, this, wallSymmetry, controller.Reticle);
                undoController.OnActionDone.Invoke(actionSetWall);
            }
        });
    }

    /// <summary>
    /// Spawns an anchor based upon the given anchor such that horizontal symmetry is maintained for the pair
    /// </summary>
    /// <param name="anchor">Anchor upon which symmetry is based</param>
    /// <returns>A list of anchors spawned</returns>
    List<GameWallAnchor> HorizontalSpawn(GameWallAnchor anchor)
    {
        List<GameWallAnchor> anchors = new List<GameWallAnchor>();
        GameWallAnchor newAnchor = anchorPool.GetObject();
        Vector3 scale = anchor.LocalScale;
        scale.y *= -1;
        Vector3 position = anchor.transform.position;
        position.y *= -1;

        newAnchor.LocalScale = scale;
        newAnchor.WallHolder = wallHolder;
        newAnchor.transform.position = position;
        newAnchor.ShouldScale = false;
        newAnchor.ShouldTrack = false;

        newAnchor.gameObject.SetActive(true);

        anchors.Add(newAnchor);
        return anchors;
    }

    /// <summary>
    /// Spawns an anchor based upon the given anchor such that vertical symmetry is maintained for the pair
    /// </summary>
    /// <param name="anchor">Anchor upon which symmetry is based</param>
    /// <returns>A list of anchors spawned</returns>
    List<GameWallAnchor> VerticalSpawn(GameWallAnchor anchor)
    {
        List<GameWallAnchor> anchors = new List<GameWallAnchor>();
        GameWallAnchor newAnchor = anchorPool.GetObject();
        Vector3 scale = anchor.LocalScale;
        scale.x *= -1;
        Vector3 position = anchor.transform.position;
        position.x *= -1;

        newAnchor.LocalScale = scale;
        newAnchor.WallHolder = wallHolder;
        newAnchor.transform.position = position;
        newAnchor.ShouldScale = false;
        newAnchor.ShouldTrack = false;

        newAnchor.gameObject.SetActive(true);

        anchors.Add(newAnchor);
        return anchors;
    }

    /// <summary>
    /// Spawns an anchor based upon the given anchor such that rotational symmetry is maintained for the pair
    /// </summary>
    /// <param name="anchor">Anchor upon which symmetry is based</param>
    /// <returns>A list of anchors spawned</returns>
    List<GameWallAnchor> RotationalSpawn(GameWallAnchor anchor)
    {
        List<GameWallAnchor> anchors = new List<GameWallAnchor>();
        GameWallAnchor newAnchor = anchorPool.GetObject();
        Vector3 scale = anchor.LocalScale;
        scale.x *= -1;
        scale.y *= -1;
        Vector3 position = anchor.transform.position;
        position.x *= -1;
        position.y *= -1;

        newAnchor.LocalScale = scale;
        newAnchor.WallHolder = wallHolder;
        newAnchor.transform.position = position;
        newAnchor.ShouldScale = false;
        newAnchor.ShouldTrack = false;

        newAnchor.gameObject.SetActive(true);

        anchors.Add(newAnchor);
        return anchors;
    }

    /// <summary>
    /// Spawns anchors based upon the given anchor such that horizontal, vertical and rotational symmetry is maintained for the group
    /// </summary>
    /// <param name="anchor">Anchor upon which symmetry is based</param>
    /// <returns>A list of anchors spawned</returns>
    List<GameWallAnchor> PerfectSpawn(GameWallAnchor anchor)
    {
        List<GameWallAnchor> anchors = new List<GameWallAnchor>();
        anchors.AddRange(RotationalSpawn(anchor));
        anchors.AddRange(VerticalSpawn(anchor));
        anchors.AddRange(HorizontalSpawn(anchor));

        return anchors;
    }

    private void Update()
    {
        if (Input.GetButtonDown("ChangeSymmetry") && shouldChange)
        {
            switch (wallSymmetry)
            {
                case WallSymmetry.NONE:
                    wallSymmetry = WallSymmetry.HORIZONTAL;
                    break;
                case WallSymmetry.HORIZONTAL:
                    wallSymmetry = WallSymmetry.VERTICAL;
                    break;
                case WallSymmetry.VERTICAL:
                    wallSymmetry = WallSymmetry.ROTATIONAL;
                    break;
                case WallSymmetry.ROTATIONAL:
                    wallSymmetry = WallSymmetry.PERFECT;
                    break;
                case WallSymmetry.PERFECT:
                    wallSymmetry = WallSymmetry.NONE;
                    break;
                default:
                    wallSymmetry = WallSymmetry.NONE;
                    break;
            }
            OnSettingChanged.Invoke(wallSymmetry);
        }
    }
}
