using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.IO;
using UnityEngine.Events;

public class LevelEditorSaveButton : MonoBehaviour
{
    [SerializeField]
    TextAsset adjectivesTxt;

    [SerializeField]
    TextAsset nounsTxt;

    [SerializeField]
    Button button;

    [SerializeField]
    ValidSpawnTester tester;

    [SerializeField]
    GameWallAnchorPool anchorPool;

    bool wasTextParsed = false;

    List<string> adjectiveList = new List<string>();

    List<string> nounList = new List<string>();

    public class Events
    {
        [Serializable]
        public class LevelCreated : UnityEvent<string> { }
    }

    public Events.LevelCreated OnLevelCreated;

    bool saveButtonUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (!saveButtonUsed)
            {
                SaveLevel();
            }
        });
        OnLevelCreated.AddListener((unused) => saveButtonUsed = true);
    }

    private void OnEnable()
    {
        saveButtonUsed = false;
    }

    void ParseFile(ref List<string> list, string file)
    {
        list = new List<string>(file.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
    }

    /// <summary>
    /// Parses the noun and adjective folders
    /// </summary>
    void ParseTextFiles()
    {
        ParseFile(ref adjectiveList, adjectivesTxt.text);
        //Debug.LogWarning(adjectiveList.AsString());
        ParseFile(ref nounList, nounsTxt.text);
        //Debug.LogWarning(nounList.AsString());
        wasTextParsed = true;
    }

    /// <summary>
    /// Get the file name for the level, in the form of
    /// adjective+adjective+noun (similar to Gfycat naming)
    /// </summary>
    /// <returns></returns>
    string GetLevelFileName()
    {
        if (!wasTextParsed)
        {
            ParseTextFiles();
        }

        TextInfo info = CultureInfo.InvariantCulture.TextInfo;

        return info.ToTitleCase(adjectiveList.GetRandomObject()) +
            info.ToTitleCase(adjectiveList.GetRandomObject()) +
            info.ToTitleCase(nounList.GetRandomObject());
    }

    /// <summary>
    /// Saves a level to the file system. The level should be located in the 
    /// StreamingAssets/Levels folder
    /// </summary>
    void SaveLevel()
    {
        string directory = Application.streamingAssetsPath + "/Levels";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string levelName = GetLevelFileName();
        string path = directory + "/" + levelName + ".xml";
        var anchors = anchorPool.GetActiveObjects();

        foreach (var anchor in anchors)
        {
            anchor.ParentWallToLevel();
        }

        LevelInfo info = tester.CreateLevel(path, levelName);

        OnLevelCreated.Invoke(levelName);

        foreach (var anchor in anchors)
        {
            anchor.ParentWallToAnchor();
        }
        if (LevelParser.Parser != null)
        {
            LevelParser.LevelStrings levelStrings;
            levelStrings.LevelName = LevelParser.Parser.AddLevel(info);
            levelStrings.LevelPath = path;
            LevelParser.Parser.DirectoryLevelDictionary.Add(levelStrings, info);
        }
    }
}
