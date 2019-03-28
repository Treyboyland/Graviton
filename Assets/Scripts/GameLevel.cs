using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class GameLevel : MonoBehaviour
{
    [SerializeField]
    TextAsset levelXml;

    [SerializeField]
    GameWallHolder holder;

    LevelInfo levelInfo;

    // Start is called before the first frame update
    void Start()
    {
        SpawnStuff();
    }

  
    void SpawnStuff()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
        using (StringReader reader = new StringReader(levelXml.text))
        {
            levelInfo = (LevelInfo)serializer.Deserialize(reader);
        }
        holder.ClearGame();
        holder.CreateLevel(levelInfo);
    }
}
