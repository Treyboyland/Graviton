using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawnRandomizer : MonoBehaviour
{
    /// <summary>
    /// Spawns all of the points
    /// </summary>
    [SerializeField]
    PointSpawner pointSpawner;

    /// <summary>
    /// The point that this spawner spawns
    /// </summary>
    [SerializeField]
    Point pointToSpawn;

    /// <summary>
    /// Seconds that will pass before the next point can spawn
    /// </summary>
    [SerializeField]
    float secondsBetweenSpawns;

    /// <summary>
    /// At or above this threshold, points will spawn
    /// </summary>
    [SerializeField]
    int comboThreshold;

    /// <summary>
    /// Seconds that will pass before the next point can spawn
    /// </summary>
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

    /// <summary>
    /// True if we should only spawn the next point after the current point has been acquired
    /// </summary>
    [SerializeField]
    bool spawnAfterPointAcquired;

    System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();

    Point currentPoint;

    /// <summary>
    /// True if this is currently spawning points
    /// </summary>
    bool spawning;


    // Start is called before the first frame update
    void Start()
    {
        startSpawnDelay = secondsBetweenSpawns;
        GameManager.Manager.OnPlayerComboUpdated.AddListener(CheckForSpawning);
        GameManager.Manager.OnResetPlayerCombo.AddListener(StopSpawning);
        if (comboThreshold == 0)
        {
            StartAppropriateSpawner();
        }
    }

    void StartAppropriateSpawner()
    {
        if(spawnAfterPointAcquired)
        {
            StartCoroutine(RandomSpawnAfterPointAcquired());
        }
        else
        {
            StartCoroutine(RandomSpawnAfterTime());
        }
    }

    void StopSpawning()
    {
        if(comboThreshold != 0)
        {
            StopAllCoroutines();
            spawning = false;
        }
    }

    void CheckForSpawning(int currentCombo)
    {
        if(!spawning && comboThreshold <= currentCombo)
        {
            StartAppropriateSpawner();
        }
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
        Point p = pointSpawner.GetGamePoint(pointToSpawn);
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

    IEnumerator RandomSpawnAfterPointAcquired()
    {
        spawning = true;
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

    IEnumerator RandomSpawnAfterTime()
    {
        spawning = true;
        while (true)
        {
            SpawnPoint();

            yield return StartCoroutine(WaitForTime());
        }
    }
}
