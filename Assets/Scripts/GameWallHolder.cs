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
    [SerializeField]
    GameWall wallPrefab;

    /// <summary>
    /// An array of walls for the game
    /// </summary>
    GameWall[] walls = new GameWall[0];

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

    /// <summary>
    /// True if we have parsed the spawn point XML
    /// </summary>
    bool pointsReceived = false;

    [SerializeField]
    TextAsset pointXml;

    SpawnLocations locations = new SpawnLocations();

    /// <summary>
    /// Places where a point may spawn in this level
    /// </summary>
    /// <value></value>
    public SpawnLocations Locations
    {
        get
        {
            return locations;
        }
        set
        {
            locations = value;
        }
    }

    /// <summary>
    /// Place where the player should spawn at the start of the game
    /// </summary>
    PlayerSpawn playerSpawn;

    public PlayerSpawn PlayerSpawn
    {
        get
        {
            return playerSpawn;
        }
        set
        {
            playerSpawn = value;
        }
    }

    /// <summary>
    /// The name that this level should have in XML
    /// </summary>
    [SerializeField]
    string levelName;

    /// <summary>
    /// The name that this level should have in XML
    /// </summary>
    /// <value></value>
    public string LevelName
    {
        get
        {
            return levelName;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //GetWalls();
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

    public void CreateLevel(LevelInfo level)
    {
        foreach (Wall wall in level.GameWalls)
        {
            GameWall childWall = Instantiate(wallPrefab, transform);
            childWall.SetParameters(wall);
        }
        levelName = level.Name;
        locations = new SpawnLocations(level.PointSpawns.Count);
        foreach (SpawnLocation location in level.PointSpawns)
        {
            locations.Add(location);
        }
        GetWalls();

    }

    public void ClearGame()
    {
        locations.Clear();
        foreach(GameWall wall in walls)
        {
            Destroy(wall.gameObject);
        }
    }
}
