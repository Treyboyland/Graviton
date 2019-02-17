using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModifiers : MonoBehaviour
{

    [SerializeField]
    float endSpeed;

    [SerializeField]
    float endSpawnDelay;

    [SerializeField]
    int maxPointsSpeed;

    [SerializeField]
    int maxPointsSpawn;

    [SerializeField]
    Player player;
    [SerializeField]
    PointSpawnRandomizer spawnRandomizer;

    // Start is called before the first frame update
    void Start()
    {
        BaseGameManager.Manager.OnPointsReceived.AddListener((score) => ModifyParameters());
    }

    void ModifyParameters()
    {
        UpdateGameSpeed();
        UpdateSpawnRate();
    }

    void UpdateGameSpeed()
    {
        player.Speed = Mathf.Lerp(player.StartSpeed, endSpeed, (float)player.Score / maxPointsSpeed);
    }

    void UpdateSpawnRate()
    {
        spawnRandomizer.SecondsBetweenSpawns = Mathf.Lerp(spawnRandomizer.StartSpawnDelay, endSpawnDelay, (float)player.Score / maxPointsSpawn);
    }

}
