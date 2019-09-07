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

    bool wasTextParsed = false;

    List<string> adjectiveList = new List<string>();

    List<string> nounList = new List<string>();

    public class Events
    {
        [Serializable]
        public class LevelCreated : UnityEvent<string> { }
    }

    public Events.LevelCreated OnLevelCreated;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(SaveLevel);
        OnLevelCreated.AddListener((unused) => button.interactable = false);
    }

    private void OnEnable()
    {
        button.interactable = true;
    }

    void ParseFile(ref List<string> list, string file)
    {
        list = new List<string>(file.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
    }

    void ParseTextFiles()
    {
        ParseFile(ref adjectiveList, adjectivesTxt.text);
        Debug.LogWarning(adjectiveList.AsString());
        ParseFile(ref nounList, nounsTxt.text);
        Debug.LogWarning(nounList.AsString());
        wasTextParsed = true;
    }

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

    void SaveLevel()
    {
        string directory = Application.streamingAssetsPath + "/Levels";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string levelName = GetLevelFileName();
        string path = directory + "/" + levelName + ".xml";
        tester.CreateLevel(path, levelName);

        OnLevelCreated.Invoke(levelName);
    }
}
