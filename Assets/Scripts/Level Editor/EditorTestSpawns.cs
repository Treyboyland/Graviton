using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTestSpawns : MonoBehaviour
{
    [SerializeField]
    ValidSpawnTester spawnTester;

    [SerializeField]
    PointSpawner pointSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("TestSpawns"))
        {
            //TODO: Check if nothing has changed?
        }    
    }


}
