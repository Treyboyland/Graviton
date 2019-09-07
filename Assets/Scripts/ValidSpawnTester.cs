using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class ValidSpawnTester : MonoBehaviour
{
    [SerializeField]
    GameWallHolder wallHolder;

    [SerializeField]
    Point point;

    [SerializeField]
    Player player;

    [SerializeField]
    Vector2 minPosition;

    [SerializeField]
    Vector2 maxPosition;

    [SerializeField]
    float increments;

    [SerializeField]
    float distanceFromWall;

    bool PointToCloseToWall(Vector3 pos)
    {
        Vector2 newPos = pos;
        int mask = LayerMask.GetMask("Game Wall");

        RaycastHit2D hitNorth = Physics2D.Linecast(newPos, newPos + Vector2.up * distanceFromWall, mask);
        RaycastHit2D hitSouth = Physics2D.Linecast(newPos, newPos + Vector2.down * distanceFromWall, mask);
        RaycastHit2D hitWest = Physics2D.Linecast(newPos, newPos + Vector2.left * distanceFromWall, mask);
        RaycastHit2D hitEast = Physics2D.Linecast(newPos, newPos + Vector2.right * distanceFromWall, mask);


        bool hit = hitNorth || hitSouth || hitEast || hitWest;
        //Debug.Log(pos + ": " + hit);

        return hit;
    }


    bool PointInWalls()
    {
        Collider2D pointCollider = point.Collider;

        foreach (GameWall wall in wallHolder.Walls)
        {
            if (pointCollider.bounds.Intersects(wall.Collider.bounds))
            {
                Debug.Log(point.transform.position + ": Point intersects: " + wall.name + " (" + wall.Collider.bounds + ")");
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// NOTE: This will modify the internal spawn list for the GameWallHolder this
    /// is associated with
    /// </summary>
    /// <returns></returns>
    public SpawnLocations GetValidSpawnLocations()
    {
        wallHolder.GetWalls();
        SpawnLocations spawnLocations = new SpawnLocations();
        float xCheck = minPosition.x;
        float yCheck = maxPosition.y;
        Collider2D pointCollider = point.Collider;
        while (yCheck >= minPosition.y)
        {
            Vector3 newPos = new Vector3(xCheck, yCheck, 0);
            point.transform.position = newPos;

            if (!PointToCloseToWall(newPos))
            {
                spawnLocations.Add(new SpawnLocation(newPos));
            }

            xCheck += increments;
            if (xCheck > maxPosition.x)
            {
                xCheck = minPosition.x;
                yCheck -= increments;
            }
        }

        return spawnLocations;
    }

    /// <summary>
    /// Returns the first possible valid spawn location for the player
    /// </summary>
    /// <returns></returns>
    public PlayerSpawn GetPlayerSpawnLocation()
    {
        wallHolder.GetWalls();

        //Start with (0,0,0) and work out
        float checkXPositive = 0;
        float checkYPositive = 0;

        float checkXNegative = 0;
        float checkYNegative = 0;

        while (checkYPositive <= maxPosition.y || checkYNegative >= minPosition.y ||
        checkXPositive <= maxPosition.x || checkXNegative >= minPosition.x)
        {
            if (checkXPositive <= maxPosition.x)
            {
                if (checkYNegative >= minPosition.y)
                {
                    Vector3 pos = new Vector3(checkXPositive, checkYNegative);
                    if (!PointToCloseToWall(pos))
                    {
                        return pos;
                    }
                }
                if (checkYPositive <= maxPosition.y)
                {
                    Vector3 pos = new Vector3(checkXPositive, checkYPositive);
                    if (!PointToCloseToWall(pos))
                    {
                        return pos;
                    }
                }
            }
            if (checkXNegative >= minPosition.x)
            {
                if (checkYNegative >= minPosition.y)
                {
                    Vector3 pos = new Vector3(checkXNegative, checkYNegative);
                    if (!PointToCloseToWall(pos))
                    {
                        return pos;
                    }
                }
                if (checkYPositive <= maxPosition.y)
                {
                    Vector3 pos = new Vector3(checkXNegative, checkYPositive);
                    if (!PointToCloseToWall(pos))
                    {
                        return pos;
                    }
                }
            }

            checkXNegative -= increments;
            checkXPositive += increments;
            checkYNegative -= increments;
            checkYPositive += increments;
        }

        Debug.LogWarning("Warning: No valid spawn location found.");

        return new Vector3();
    }

    public void CreateLevel()
    {
        SpawnLocations spawnLocations = GetValidSpawnLocations();
        wallHolder.Locations = spawnLocations;
        wallHolder.PlayerSpawn = GetPlayerSpawnLocation();

        LevelInfo info = new LevelInfo(wallHolder);

        SaveXml(info);

    }

    public void CreateLevel(string path)
    {
        SpawnLocations spawnLocations = GetValidSpawnLocations();
        wallHolder.Locations = spawnLocations;
        wallHolder.PlayerSpawn = GetPlayerSpawnLocation();

        LevelInfo info = new LevelInfo(wallHolder);

        SaveXml(info, path);
    }

    public void RunTest()
    {
        SpawnLocations spawnLocations = GetValidSpawnLocations();

        Debug.Log("Valid Locations: " + spawnLocations.Count);
        if (spawnLocations.Count != 0)
        {
            SaveXml(spawnLocations);
        }
    }

    string GetFileName()
    {
        string dirString = Application.dataPath + "\\..\\Spawns";
        if (!Directory.Exists(dirString))
        {
            Directory.CreateDirectory(dirString);
        }
        return dirString + "\\Spawns-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".xml";
    }

    string GetLevelFileName(string name)
    {
        string dirString = Application.dataPath + "\\..\\Spawns";
        if (!Directory.Exists(dirString))
        {
            Directory.CreateDirectory(dirString);
        }
        return dirString + "\\Level-" + name + "-" + DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss-fff") + ".xml";
    }

    void SaveXml(SpawnLocations locations)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpawnLocations));

        string filename = GetFileName();

        using (TextWriter tw = new StreamWriter(filename))
        {
            serializer.Serialize(tw, locations);
            Debug.LogWarning("Spawns Saved to file: \"" + filename + "\"");
        }
    }

    void SaveXml(LevelInfo level, string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));

        using (TextWriter tw = new StreamWriter(path))
        {
            serializer.Serialize(tw, level);
            Debug.LogWarning("Level Saved to file: \"" + path + "\"");
        }
    }

    void SaveXml(LevelInfo level)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));

        string filename = GetLevelFileName(level.Name);

        using (TextWriter tw = new StreamWriter(filename))
        {
            serializer.Serialize(tw, level);
            Debug.LogWarning("Level Saved to file: \"" + filename + "\"");
        }
    }


}
