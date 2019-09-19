using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;

/// <summary>
/// Contains information on the configuration for the game
/// </summary>
[XmlRootAttribute("GameConfig")]
[Serializable]
public struct GameConfig
{
    /// <summary>
    /// True if we should allow level creation in the game
    /// </summary>
    [XmlElement("AllowLevelCreation")]
    [Tooltip("True if we should allow level creation in the game")]
    public bool AllowLevelCreation;
    /// <summary>
    /// True if we should allow level deletion in the game
    /// </summary>
    [XmlElement("AllowLevelDeletion")]
    [Tooltip("True if we should allow level deletion in the game")]
    public bool AllowLevelDeletion;
    /// <summary>
    /// True if we should prevent exiting from the game
    /// </summary>
    [XmlElement("AllowExiting")]
    [Tooltip("True if we should prevent exiting from the game")]
    public bool AllowExiting;

    [XmlElement("IsArcadeCabinet")]
    [Tooltip("True if this game is going on an arcade cabinet")]
    public bool IsArcadeCabinet;


    /// <summary>
    /// Saves this config to the given path
    /// </summary>
    /// <param name="path"></param>
    public void Save(string path)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GameConfig));
            using (TextWriter tw = new StreamWriter(path))
            {
                serializer.Serialize(tw, this);
                Debug.LogWarning("Game config saved to \"" + path + "\"");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ERROR: Unable to save game config to path \"" + path + "\": " + e);
        }
    }
}