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

    string additionalLevelsPath;

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
        set
        {
            areLevelsParsed = value;
        }
    }

    /// <summary>
    /// Returns progress of the load operation, if loading asynchronously
    /// </summary>
    float progress = 0;

    /// <summary>
    /// Returns progress of the load operation, if loading asynchronously
    /// </summary>
    /// <value></value>
    public float Progress
    {
        get
        {
            return progress;
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
        additionalLevelsPath = Application.streamingAssetsPath + "/Levels";
        //ParseLevels();
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

    public void ParseLevelsAsync()
    {
        StopAllCoroutines();
        StartCoroutine(ParseLevelsWithProgress());
    }

    IEnumerator ParseLevelsWithProgress()
    {
        progress = 0;
        int count = 0;


        //NOTE: This won't work for mobile
        string[] filePaths = new string[0];
        try
        {
            filePaths = Directory.GetFiles(additionalLevelsPath);
        }
        catch (Exception e)
        {
            Debug.LogError("ERROR: Unable to get file paths fron \"" + additionalLevelsPath + "\": " + e);
        }

        int total = filePaths.Length + levelXmls.Count;

        levelDictionary.Clear();

        foreach (TextAsset levelXml in levelXmls)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
            using (StringReader reader = new StringReader(levelXml.text))
            {
                LevelInfo levelInfo = (LevelInfo)serializer.Deserialize(reader);
                string name = levelInfo.Name;
                while (levelDictionary.ContainsKey(name))
                {
                    name = GetNextLevelString(name);
                }

                levelDictionary.Add(name, levelInfo);
            }

            count++;
            progress = 1.0f * count / total;
            yield return null;
        }


        foreach (string filePath in filePaths)
        {

            count++;
            progress = 1.0f * count / total;
            string levelName = "";
            if (string.Compare(Path.GetExtension(filePath), ".xml", true) != 0)
            {
                yield return null;
                continue;
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
                using (StringReader reader = new StringReader(File.ReadAllText(filePath)))
                {
                    LevelInfo levelInfo = (LevelInfo)serializer.Deserialize(reader);
                    string name = levelInfo.Name;
                    levelName = name;
                    while (levelDictionary.ContainsKey(name))
                    {
                        name = GetNextLevelString(name);
                        levelName = name;
                    }
                    levelDictionary.Add(name, levelInfo);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("Unable to parse file \"" + filePath + "\": " + e);
                Debug.LogWarning("Level name: " + levelName);
            }

            yield return null;
        }




        areLevelsParsed = true;
        progress = 1;

        Debug.LogWarning("Levels successfully parsed: " + levelDictionary.Count);
    }

    public void AddLevel(LevelInfo info)
    {
        string name = info.Name;
        while (levelDictionary.ContainsKey(name))
        {
            name = GetNextLevelString(name);
        }
        levelDictionary.Add(name, info);
    }

    public void ParseLevels()
    {
        //TODO: Do we want to add the ability to add to this from the file system?
        levelDictionary.Clear();

        foreach (TextAsset levelXml in levelXmls)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
            using (StringReader reader = new StringReader(levelXml.text))
            {
                LevelInfo levelInfo = (LevelInfo)serializer.Deserialize(reader);
                string name = levelInfo.Name;
                while (levelDictionary.ContainsKey(name))
                {
                    name = GetNextLevelString(name);
                }

                levelDictionary.Add(name, levelInfo);
            }
        }

        //NOTE: This won't work for mobile
        try
        {
            foreach (string filePath in Directory.GetFiles(additionalLevelsPath))
            {
                string levelName = "";
                if (string.Compare(Path.GetExtension(filePath), ".xml", true) != 0)
                {
                    continue;
                }
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LevelInfo));
                    using (StringReader reader = new StringReader(File.ReadAllText(filePath)))
                    {
                        LevelInfo levelInfo = (LevelInfo)serializer.Deserialize(reader);
                        string name = levelInfo.Name;
                        levelName = name;
                        while (levelDictionary.ContainsKey(name))
                        {
                            name = GetNextLevelString(name);
                            levelName = name;
                        }
                        levelDictionary.Add(name, levelInfo);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning("Unable to parse file \"" + filePath + "\": " + e);
                    Debug.LogWarning("Level name: " + levelName);
                    continue;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("We are unable to load external files for some reason: " + e);
        }



        areLevelsParsed = true;

        Debug.LogWarning("Levels successfully parsed: " + levelDictionary.Count);
    }
}
