using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Reads the configuration for the game
/// </summary>
public class GameConfigReader : MonoBehaviour
{

    /// <summary>
    /// Configuration loaded for the game
    /// </summary>
    [SerializeField]
    GameConfig configuration;

    public GameConfig Configuration
    {
        get
        {
            return configuration;
        }
    }

    /// <summary>
    /// Fallback if for some reason we are unable to load the file system
    /// </summary>
    /// <returns></returns>
    [SerializeField]
    GameConfig defaultConfig;

    static GameConfigReader _instance;

    public static GameConfigReader ConfigReader
    {
        get
        {
            return _instance;
        }
    }

    string configPath;

    private void Awake()
    {
        if (_instance != null && this != _instance)
        {
            Destroy(gameObject);
            return;
        }
        configPath = Application.streamingAssetsPath + "/Config.xml";
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadConfig();
    }

    void ReadConfig()
    {

        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (TextReader reader = new StreamReader(configPath))
            {
                configuration = (GameConfig)serializer.Deserialize(reader);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ERROR: Unable to read config from path \"" + configPath + "\"\r\n " + e + "\r\n Defaulting to default config.");
            configuration = defaultConfig;
        }

    }
}
