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

    // Start is called before the first frame update
    void Start()
    {

    }

    void TestSpawns()
    {
        //TODO: Check if nothing has changed?
        pointSpawner.DisableAllPoints();
        var anchors = anchorPool.GetActiveObjects();
        foreach (var anchor in anchors)
        {

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
