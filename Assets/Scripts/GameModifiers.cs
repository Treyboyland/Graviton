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
    [Range(0, 1)]
    float speedCutter;

    [SerializeField]
    Player player;
    [SerializeField]
    PointSpawnRandomizer spawnRandomizer;

    // Start is called before the first frame update
    void Start()
    {
        BaseGameManager.Manager.OnPointsReceived.AddListener((points) => ModifyParameters(points));
        BaseGameManager.Manager.OnPlayerTakeDamage.AddListener(SlowDownPlayer);
    }

    void ModifyParameters(int points)
    {
        //UpdateGameSpeed();
        IncreasePlayerSpeed(points);
        UpdateSpawnRate();
    }

    void SlowDownPlayer()
    {
        player.Speed = player.Speed - (player.Speed - player.MinSpeed) * speedCutter;
    }

    void UpdateGameSpeed()
    {
        player.Speed = Mathf.Lerp(player.StartSpeed, endSpeed, (float)player.Score / maxPointsSpeed);
    }

    void IncreasePlayerSpeed(int points)
    {
        player.Speed += Mathf.Abs(player.MaxSpeed - player.MinSpeed) * points / 2000;
    }

    void UpdateSpawnRate()
    {
        spawnRandomizer.SecondsBetweenSpawns = Mathf.Lerp(spawnRandomizer.StartSpawnDelay, endSpawnDelay, (float)player.Score / maxPointsSpawn);
    }

}
