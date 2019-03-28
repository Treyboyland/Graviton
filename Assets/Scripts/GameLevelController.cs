using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{
    [SerializeField]
    List<GameLevelAndType> levelAndTypes;

    //TODO: I might want to make this a string to prefab holder, as opposed to enum
    Dictionary<GameLevelType, GameWallHolder> levelDictionary;

    // Start is called before the first frame update
    void Start()
    {
        ParseLevels();
    }

    void ParseLevels()
    {
        foreach(GameLevelAndType level in levelAndTypes)
        {
            GameWallHolder holder = Instantiate(level.Level, transform) as GameWallHolder;
            holder.gameObject.SetActive(false);
            levelDictionary.Add(level.Type, holder);
        }
    }
}
