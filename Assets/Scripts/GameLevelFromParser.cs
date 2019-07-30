using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelFromParser : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    GameWallHolder holder;

    // Start is called before the first frame update
    void Start()
    {
        SpawnStuff();
    }

  
    void SpawnStuff()
    {
        LevelInfo info = LevelParser.Parser.LevelDictionary[LevelParser.Parser.ChosenLevel];
        holder.ClearGame();
        holder.CreateLevel(info);
        player.transform.position = info.PlayerSpawn;
    }
}
