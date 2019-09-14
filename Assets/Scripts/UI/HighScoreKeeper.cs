using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System;
using UnityEngine.Events;
using System.IO;

public class HighScoreKeeper : MonoBehaviour
{

    public class Events
    {
        [Serializable]
        public class HighScoreSaveFail : UnityEvent { }
        [Serializable]
        public class NewHighScore : UnityEvent<bool, int> { }
    }

    [SerializeField]
    Player player;

    public Events.HighScoreSaveFail OnHighScoreSaveFail;

    public Events.NewHighScore OnNewHighScore;

    string highScorePath;

    Dictionary<string, int> highScores = new Dictionary<string, int>();

    bool highScoresLoaded = false;

    private void Awake()
    {
        highScorePath = Application.persistentDataPath + "\\HighScores.xml";

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(highScorePath);
        GameManager.Manager.OnGameOver.AddListener(() => CheckHighScore(LevelParser.Parser.LevelDictionary[LevelParser.Parser.ChosenLevel], player.Score));

    }

    void LoadHighScores()
    {
        try
        {
            if (File.Exists(highScorePath))
            {
                XElement doc = XElement.Load(highScorePath);
                foreach (XElement element in doc.Descendants())
                {
                    if (string.Compare(element.Name.LocalName, "HighScore") == 0)
                    {
                        highScores.Add(element.Attribute("Level").Value, int.Parse(element.Attribute("Score").Value));
                    }
                }
            }
            else
            {
                XElement doc = new XElement("HighScores");
                doc.Save(highScorePath);
            }
            highScoresLoaded = true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    void SaveHighScoreData()
    {
        XElement doc = new XElement("HighScores");


        foreach (string levelName in highScores.Keys)
        {
            doc.Add(GetHighScoreEntry(levelName, highScores[levelName]));
        }

        doc.Save(highScorePath);
    }

    XElement GetHighScoreEntry(string levelName, int score)
    {
        XElement element = new XElement("HighScore");
        element.Add(new XAttribute("Level", levelName));
        element.Add(new XAttribute("Score", score));
        return element;
    }

    public int GetScoreForLevel(LevelInfo level)
    {
        return !highScores.ContainsKey(level.Name) ? -1 : highScores[level.Name];
    }


    void CheckHighScore(LevelInfo info, int score)
    {
        if (!highScoresLoaded)
        {
            LoadHighScores();
        }
        if (!highScoresLoaded)
        {
            OnHighScoreSaveFail.Invoke();
            return;
        }

        if (!highScores.ContainsKey(info.Name))
        {
            highScores.Add(info.Name, score);
            SaveHighScoreData();
            OnNewHighScore.Invoke(true, -1);
        }
        else if (highScores[info.Name] < score)
        {
            int oldHighScore = highScores[info.Name];
            highScores[info.Name] = score;
            try
            {
                SaveHighScoreData();
                OnNewHighScore.Invoke(true, oldHighScore);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                OnHighScoreSaveFail.Invoke();
            }


        }
        else
        {
            OnNewHighScore.Invoke(false, highScores[info.Name]);
        }
    }
}
