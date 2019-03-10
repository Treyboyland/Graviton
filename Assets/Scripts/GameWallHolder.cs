using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the walls for the game
/// </summary>
public class GameWallHolder : MonoBehaviour
{
    /// <summary>
    /// An array of walls for the game
    /// </summary>
    GameWall[] walls;

    public GameWall[] Walls
    {
        get
        {
            return walls;
        }
    }

    /// <summary>
    /// True if we have the walls
    /// </summary>
    bool wallsGotten = false;

    // Start is called before the first frame update
    void Start()
    {
        GetWalls();
    }

    /// <summary>
    /// Get all of the child walls of this game object
    /// </summary>
    public void GetWalls()
    {
        if (!wallsGotten)
        {
            wallsGotten = true;
            walls = GetComponentsInChildren<GameWall>();
        }
    }
}
