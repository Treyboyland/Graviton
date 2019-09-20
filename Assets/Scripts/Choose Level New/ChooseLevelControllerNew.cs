﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ChooseLevelControllerNew : MonoBehaviour
{
    [SerializeField]
    GameObject chooseLevelCanvas;

    [SerializeField]
    Button levelSelectButton;

    [SerializeField]
    Button startButton;

    [SerializeField]
    LevelPreviewer previewer;

    [SerializeField]
    List<GameObject> arrows;

    [SerializeField]
    TextMeshProUGUI levelNameTextBox;

    [SerializeField]
    TextMeshProUGUI countTextBox;

    List<LevelInfo> levels = new List<LevelInfo>();

    int currentIndex = 0;

    bool levelsParsed = false;

    bool joystickEventConsumed = false;

    // Start is called before the first frame update
    void Start()
    {
        HideCanvas();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == levelSelectButton.gameObject)
        {
            EnableArrows();
            HandleLevelMove();
        }
        else
        {
            DisableArrows();
        }
    }

    void DisableArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }

    void EnableArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(true);
        }
    }

    void HandleLevelMove()
    {
        //NOTE: Arrows sort of look weird when there is only one level...but there will never be only one level
        if (Input.GetButtonDown("Left") || (!joystickEventConsumed && Input.GetAxis("LeftRightJoy") < 0))
        {
            joystickEventConsumed = true;
            PreviousLevel();
        }
        else if (Input.GetButtonDown("Right") || (!joystickEventConsumed && Input.GetAxis("LeftRightJoy") > 0))
        {
            joystickEventConsumed = true;
            NextLevel();
        }

        if (Input.GetAxis("LeftRightJoy") == 0)
        {
            joystickEventConsumed = false;
        }
        // else if (Input.GetButtonDown("Submit"))
        // {
        //     //TODO: Load selected level
        //     LevelParser.Parser.ChosenLevel = levels[currentIndex].Name;
        //     SceneLoader.Loader.LoadMainGameScene();
        // }
    }

    public void LoadSelectedLevel()
    {
        //TODO: Load selected level
        LevelParser.Parser.ChosenLevel = levels[currentIndex].Name;
        SceneLoader.Loader.LoadMainGameScene();
    }

    /// <summary>
    /// Sorts levels alphabetically
    /// </summary>
    void ParseAndSortDictionary()
    {
        if (!levelsParsed)
        {
            if (!LevelParser.Parser.AreLevelsParsed) //Facilitates launch from main as opposed to loading screen -- This should normally not be called
            {
                LevelParser.Parser.ParseLevels();
            }
            levelsParsed = true;
            levels.Clear();
            foreach (LevelInfo info in LevelParser.Parser.LevelDictionary.Values)
            {
                levels.Add(info);
            }

            levels.Sort((a, b) => a.Name.CompareTo(b.Name));
        }
    }


    /// <summary>
    /// Shows the level select canvas, and selects the invisible level select button
    /// </summary>
    public void ShowCanvas()
    {
        ParseAndSortDictionary();
        EnableArrows();
        previewer.GenerateLevel(levels[currentIndex]);
        countTextBox.text = "" + (currentIndex + 1) + "/" + levels.Count;
        levelNameTextBox.text = levels[currentIndex].Name;
        chooseLevelCanvas.gameObject.SetActive(true);
        levelSelectButton.Select();
    }

    /// <summary>
    /// Hides the level select canvas and selects the start button
    /// </summary>
    public void HideCanvas()
    {
        chooseLevelCanvas.gameObject.SetActive(false);
        startButton.Select();
        previewer.DisableCurrentWallsAndClearList();
    }

    /// <summary>
    /// View the next level
    /// </summary>
    public void NextLevel()
    {
        if (currentIndex + 1 < levels.Count)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = 0;
        }
        previewer.GenerateLevel(levels[currentIndex]);
        countTextBox.text = "" + (currentIndex + 1) + "/" + levels.Count;
        levelNameTextBox.text = levels[currentIndex].Name;
    }

    /// <summary>
    /// View the previous level
    /// </summary>
    public void PreviousLevel()
    {
        if (currentIndex - 1 >= 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex = levels.Count - 1;
        }
        previewer.GenerateLevel(levels[currentIndex]);
        countTextBox.text = "" + (currentIndex + 1) + "/" + levels.Count;
        levelNameTextBox.text = levels[currentIndex].Name;
    }

}
