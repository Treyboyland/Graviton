using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreviewer : MonoBehaviour
{
    [SerializeField]
    GameWallPool wallPool;

    string levelName = "";

    List<GameWall> walls = new List<GameWall>();

    // Start is called before the first frame update
    void Start()
    {

    }

    public void DisableCurrentWallsAndClearList()
    {
        foreach (GameWall wall in walls)
        {
            wall.gameObject.SetActive(false);
        }
        walls.Clear();
    }

    public void GenerateLevel(LevelInfo level)
    {
        DisableCurrentWallsAndClearList();
        foreach (Wall wall in level.GameWalls)
        {
            GameWall childWall = wallPool.GetObject();
            childWall.transform.SetParent(transform);
            childWall.SetParameters(wall);
            childWall.gameObject.SetActive(true);
            walls.Add(childWall);
        }
        levelName = level.Name;
    }
}
