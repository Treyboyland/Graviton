using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;
using System.IO;

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

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(SaveLevel);
    }

    void ParseFile(List<string> list, string file)
    {
        list = new List<string>(file.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));
    }

    void ParseTextFiles()
    {
        ParseFile(adjectiveList, adjectivesTxt.text);
        ParseFile(nounList, nounsTxt.text);
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
            info.ToTitleCase(nounList.GetRandomObject()) + ".xml";
    }

    void SaveLevel()
    {
        string directory = Application.streamingAssetsPath + "/Levels";
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string path = directory + "/" + GetLevelFileName();

        tester.CreateLevel(path);
    }
}
