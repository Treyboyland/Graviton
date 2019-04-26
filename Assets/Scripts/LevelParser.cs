using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System;

public class LevelParser : MonoBehaviour
{
    /// <summary>
    /// Levels in the game
    /// </summary>
    [SerializeField]
    List<TextAsset> levelXmls;

    List<LevelInfo> levels;

    Dictionary<string, LevelInfo> levelDictionary = new Dictionary<string, LevelInfo>();

    /// <summary>
    /// Contains all the levels loaded for the game
    /// </summary>
    /// <value></value>
    public Dictionary<string, LevelInfo> LevelDictionary
    {
        get
        {
            return levelDictionary;
        }
    }


    string chosenLevel;

    /// <summary>
    /// Name of the chosen level
    /// </summary>
    /// <value></value>
    public string ChosenLevel
    {
        get
        {
            return chosenLevel;
        }
        set
        {
            chosenLevel = value;
        }
    }

    static LevelParser _instance;

    public static LevelParser Parser
    {
        get
        {
            return _instance;
        }
    }

    /// <summary>
    /// True if the levels have been parsed
    /// </summary>
    bool areLevelsParsed;

    /// <summary>
    /// True if the levels have been parsed
    /// </summary>
    /// <value></value>
    public bool AreLevelsParsed
    {
        get
        {
            return areLevelsParsed;
        }
    }

    void Awake()
    {
        if (_instance != null && this != _instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        ParseLevels();
    }

    string GetName(string root, int num)
    {
        return root + "-" + string.Format("{0:D2}", num);
    }

    string GetNextLevelString(string name)
    {
        int index = name.LastIndexOf('-');
        if (index != -1)
        {
            if (index == name.Length - 1) //Name is xxxx-
            {
                return name + "01";
            }
            try
            {
                string root = name.Substring(0, index);
                int num = int.Parse(name.Substring(index + 1));
                string newName;

                do
                {
                    num++;
                    newName = GetName(root, num);
                }
                while (levelDictionary.ContainsKey(newName));

                return newName;
            }
            catch (Exception)
            {
                return name + "-01";
            }
        }
        return name + "-01";
    }

    public void ParseLevels()
    {
        //TODO: Do we want to add the ability to add to this from the file system?

        foreach (TextAsset levelXml in levelXmls)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
            using (StringReader reader = new StringReader(levelXml.text))
            {
                LevelInfo levelInfo = (LevelInfo)serializer.Deserialize(reader);
                string name = levelInfo.Name;
                if (levelDictionary.ContainsKey(name))
                {
                    name = GetNextLevelString(name);
                }

                levelDictionary.Add(name, levelInfo);
            }
        }

        areLevelsParsed = true;

        Debug.LogWarning("Levels successfully parsed: " + levelDictionary.Count);
    }
}
