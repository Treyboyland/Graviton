using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWallAnchor : MonoBehaviour
{
    /// <summary>
    /// Game Wall to be anchored by this anchor
    /// </summary>
    [SerializeField]
    GameWall gameWall;

    /// <summary>
    /// Level that the game wall should be parented to on destruction
    /// </summary>
    GameLevel gameLevel;

    /// <summary>
    /// Level that the game wall should be parented to on destruction
    /// </summary>
    public GameLevel GameLevel
    {
        get
        {
            return gameLevel;
        }
        set
        {
            gameLevel = value;
        }
    }

    public Vector3 LocalScale
    {
        get
        {
            return transform.localScale;
        }
        set
        {
            transform.localScale = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
