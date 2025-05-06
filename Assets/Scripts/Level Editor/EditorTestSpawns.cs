using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTestSpawns : MonoBehaviour
{
    [SerializeField]
    ValidSpawnTester spawnTester;

    [SerializeField]
    GameWallAnchorPool anchorPool;

    [SerializeField]
    PointSpawner pointSpawner;

    [SerializeField]
    Point pointPrefab;

    bool shouldTest = true;

    bool spawnsShowing = false;

    public bool ShouldTest
    {
        get
        {
            return shouldTest;
        }
        set
        {
            shouldTest = value;
        }
    }

    public void RemovePoints()
    {
        pointSpawner.DisableAllPoints();
        spawnsShowing = false;
    }

    void TestSpawns()
    {
        //TODO: Check if nothing has changed?
        pointSpawner.DisableAllPoints();
        var anchors = anchorPool.GetActiveObjects();
        foreach (var anchor in anchors)
        {
            anchor.ParentWallToLevel();
        }

        var spawns = spawnTester.GetValidSpawnLocations();

        foreach (var spawn in spawns)
        {
            Point p = pointSpawner.GetGamePoint(pointPrefab);
            p.transform.position = spawn;
            p.gameObject.SetActive(true);
        }

        foreach (var anchor in anchors)
        {
            anchor.ParentWallToAnchor();
        }

        spawnsShowing = true;
    }

    /// <summary>
    /// Should be set to the Test Spawns event
    /// </summary>
    public void HandleTestSpawnAction()
    {
        if (shouldTest)
        {
            if (!spawnsShowing)
            {
                TestSpawns();
            }
            else
            {
                RemovePoints();
            }
        }
    }
}
