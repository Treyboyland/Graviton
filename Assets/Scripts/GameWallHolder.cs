using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

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

    bool pointsReceived;

    [SerializeField]
    TextAsset pointXml;

    SpawnLocations locations;

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

    public void SetWallsDamaging(bool val)
    {
        if (!wallsGotten)
        {
            GetWalls();
        }

        foreach (GameWall wall in walls)
        {
            wall.IsDamaging = val;
        }
    }

    void ParsePoints()
    {
        pointsReceived = true;

        XmlSerializer serializer = new XmlSerializer(typeof(SpawnLocations));

        using (StringReader reader = new StringReader(pointXml.text))
        {
            locations = (SpawnLocations)serializer.Deserialize(reader);
        }

        

    }

    public SpawnLocations GetSpawnLocations()
    {
        if (!pointsReceived)
        {
            ParsePoints();
        }

        return new SpawnLocations(locations);
    }
}
