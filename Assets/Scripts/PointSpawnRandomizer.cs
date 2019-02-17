using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnRandomizer : MonoBehaviour
{
    [SerializeField]
    PointSpawner pointSpawner;

    [SerializeField]
    float secondsBetweenSpawns;

    public float SecondsBetweenSpawns
    {
        get
        {
            return secondsBetweenSpawns;
        }
        set
        {
            secondsBetweenSpawns = value;
        }
    }

    /// <summary>
    /// Spawn delay at the start of the game
    /// </summary>
    float startSpawnDelay;


    /// <summary>
    /// Spawn delay at the start of the game
    /// </summary>
    /// <value></value>
    public float StartSpawnDelay
    {
        get
        {
            return startSpawnDelay;
        }
    }

    [SerializeField]
    Vector2 minSpawnLocation;

    [SerializeField]
    Vector2 maxSpawnLocation;

    System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    Point currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        startSpawnDelay = secondsBetweenSpawns;
        StartCoroutine(RandomSpawnPeriodically());
    }
    IEnumerator WaitForTime()
    {
        timer.Reset();
        timer.Start();

        while (timer.Elapsed.TotalSeconds < secondsBetweenSpawns)
        {
            yield return null;
        }
    }

    void SpawnPoint()
    {
        Point p = pointSpawner.GetGamePoint();
        Vector2 newLoc = GetRandomLocation();
        p.transform.position = newLoc;
        p.gameObject.SetActive(true);
        currentPoint = p;
    }

    Vector2 GetRandomLocation()
    {
        float x = UnityEngine.Random.Range(minSpawnLocation.x, maxSpawnLocation.x);
        float y = UnityEngine.Random.Range(minSpawnLocation.y, maxSpawnLocation.y);

        return new Vector2(x, y);
    }

    IEnumerator RandomSpawnPeriodically()
    {
        while (true)
        {
            SpawnPoint();

            while (currentPoint.gameObject.activeInHierarchy)
            {
                yield return null;
            }

            yield return StartCoroutine(WaitForTime());

        }
    }
}
