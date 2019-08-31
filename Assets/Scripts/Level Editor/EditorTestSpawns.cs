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

    // Start is called before the first frame update
    void Start()
    {

    }

    public void RemovePoints()
    {
        pointSpawner.DisableAllPoints();
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TestSpawns"))
        {
            TestSpawns();
        }
    }


}
