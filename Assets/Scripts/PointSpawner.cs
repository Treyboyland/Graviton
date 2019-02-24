using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PointSpawner : MonoBehaviour
{
    [SerializeField]
    List<PointPoolInitializer> initializers;

    Dictionary<Point, List<Point>> pointPools = new Dictionary<Point, List<Point>>();


    [Serializable]
    public struct PointPoolInitializer
    {
        public Point pointPrefab;
        public int initialPoolSize;
    }

    void Awake()
    {
       InitializePool();
    }

    void InitializePool()
    {
        foreach (PointPoolInitializer ppi in initializers)
        {
            if (!pointPools.ContainsKey(ppi.pointPrefab))
            {
                pointPools.Add(ppi.pointPrefab, new List<Point>());
            }

            for (int i = 0; i < ppi.initialPoolSize; i++)
            {
                Point p = Instantiate(ppi.pointPrefab) as Point;
                p.gameObject.SetActive(false);
                pointPools[ppi.pointPrefab].Add(p);
            }
        }
    }


    public Point GetGamePoint(Point point)
    {
        if(!pointPools.ContainsKey(point))
        {
            pointPools.Add(point, new List<Point>());
        }
        for (int i = 0; i < pointPools[point].Count; i++)
        {
            if (!pointPools[point][i].gameObject.activeInHierarchy)
            {
                return pointPools[point][i];
            }
        }

        Point p = Instantiate(point) as Point;
        p.gameObject.SetActive(false);
        pointPools[point].Add(p);

        return p;
    }
}


