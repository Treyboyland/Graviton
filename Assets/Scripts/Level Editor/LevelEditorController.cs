using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelEditorController : MonoBehaviour
{
    [SerializeField]
    ReticleController reticle;

    [SerializeField]
    GameWallAnchorPool wallAnchorPool;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleActions();
    }

    /// <summary>
    /// Returns all game walls with a collider that overlaps the given position
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    List<GameWall> GetGameWallsWithPoint(Vector3 position)
    {
        GameObject[] wallObj = GameObject.FindGameObjectsWithTag("GameWall");
        List<GameWall> gameWalls = new List<GameWall>(wallObj.Length);

        for (int i = 0; i < wallObj.Length; i++)
        {
            gameWalls.Add(wallObj[i].GetComponent<GameWall>());
        }

        List<GameWall> toReturn = new List<GameWall>();
        foreach (GameWall wall in gameWalls)
        {
            if (wall.ContainsPoint(position))
            {
                toReturn.Add(wall);
            }
        }

        return toReturn;
    }

    void HandleActions()
    {
        if (Input.GetButtonDown("Submit"))
        {
            //NOTE: I want to be able to toggle damaging 
            if (!reticle.IsFlickering)
            {
                List<GameWall> walls = GetGameWallsWithPoint(reticle.transform.position);
                if (walls.Count == 0)
                {
                    //TODO: Place Wall
                }
                else
                {
                    foreach (GameWall wall in walls)
                    {
                        wall.IsDamaging = !wall.IsDamaging;
                    }
                }
            }
        }
    }

}
