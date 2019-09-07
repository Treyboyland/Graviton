using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class SymmetricWallPlacer : MonoBehaviour
{

    public class Events
    {
        [Serializable]
        public class SettingChanged : UnityEvent<WallSymmetry> { }
    }

    [SerializeField]
    LevelEditorController controller;

    [SerializeField]
    GameWallAnchorPool anchorPool;

    [SerializeField]
    GameWallHolder wallHolder;

    bool shouldChange = true;

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

    public enum WallSymmetry
    {
        NONE,
        VERTICAL,
        HORIZONTAL,
        ROTATIONAL
    }

    [SerializeField]
    WallSymmetry wallSymmetry;

    public WallSymmetry Symmetry
    {
        get
        {
            return wallSymmetry;
        }
        set
        {
            wallSymmetry = value;
        }
    }

    public Events.SettingChanged OnSettingChanged;

    // Start is called before the first frame update
    void Start()
    {
        controller.OnWallPlaced.AddListener((anchor) =>
        {
            if (anchor != null)
            {
                switch (wallSymmetry)
                {
                    case WallSymmetry.HORIZONTAL:
                        HorizontalSpawn(anchor);
                        break;
                    case WallSymmetry.ROTATIONAL:
                        RotationalSpawn(anchor);
                        break;
                    case WallSymmetry.VERTICAL:
                        VerticalSpawn(anchor);
                        break;
                    case WallSymmetry.NONE:
                    default:
                        break;
                }
            }
        });
    }

    void HorizontalSpawn(GameWallAnchor anchor)
    {
        GameWallAnchor newAnchor = anchorPool.GetObject();
        Vector3 scale = anchor.LocalScale;
        scale.y *= -1;
        Vector3 position = anchor.transform.position;
        position.y *= -1;

        newAnchor.LocalScale = scale;
        newAnchor.WallHolder = wallHolder;
        newAnchor.transform.position = position;

        newAnchor.gameObject.SetActive(true);
    }

    void VerticalSpawn(GameWallAnchor anchor)
    {
        GameWallAnchor newAnchor = anchorPool.GetObject();
        Vector3 scale = anchor.LocalScale;
        scale.x *= -1;
        Vector3 position = anchor.transform.position;
        position.x *= -1;

        newAnchor.LocalScale = scale;
        newAnchor.WallHolder = wallHolder;
        newAnchor.transform.position = position;

        newAnchor.gameObject.SetActive(true);
    }

    void RotationalSpawn(GameWallAnchor anchor)
    {
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

        newAnchor.gameObject.SetActive(true);
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
