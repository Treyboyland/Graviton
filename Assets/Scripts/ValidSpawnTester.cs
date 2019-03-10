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
    Vector2 minPosition;

    [SerializeField]
    Vector2 maxPosition;

    [SerializeField]
    float increments;

    [SerializeField]
    float distanceFromWall;

    // Start is called before the first frame update
    void Start()
    {
        wallHolder.GetWalls();
        RunTest();
    }

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
                Debug.Log(point.transform.position + ": Point intersects: " + wall.name  + " (" + wall.Collider.bounds + ")");
                return true;
            }
        }

        return false;
    }

    void RunTest()
    {
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

    void SaveXml(SpawnLocations locations)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SpawnLocations));

        using (TextWriter tw = new StreamWriter(GetFileName()))
        {
            serializer.Serialize(tw, locations);
        }
    }
}
