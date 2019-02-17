using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    [SerializeField]
    int initialPoolSize;

    [SerializeField]
    Point pointPrefab;

    List<Point> pointPool = new List<Point>();
    
    void Awake()
    {
        InitializePool();
    }

    void InitializePool()
    {
        for(int i = 0; i < initialPoolSize; i++)
        {
            Point p = Instantiate(pointPrefab) as Point;
            p.gameObject.SetActive(false);
            pointPool.Add(p);
        }
    }

    public Point GetGamePoint()
    {
        for(int i =0; i < pointPool.Count; i++)
        {
            if(!pointPool[i].gameObject.activeInHierarchy)
            {
                return pointPool[i];
            }
        }

        Point p = Instantiate(pointPrefab) as Point;
        p.gameObject.SetActive(false);
        pointPool.Add(p);

        return p;
    }
}
