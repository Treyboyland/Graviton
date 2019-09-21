using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using System.IO;

public class DeleteLevelController : MonoBehaviour
{
    [SerializeField]
    GameObject deleteLevel;

    [SerializeField]
    GameObject confirmation;

    [SerializeField]
    Button levelSelectButton;

    [SerializeField]
    Button deleteButton;

    [SerializeField]
    Button backButton;

    [SerializeField]
    LevelPreviewer previewer;

    [SerializeField]
    List<GameObject> arrows;

    [SerializeField]
    TextMeshProUGUI levelNameTextBox;

    [SerializeField]
    TextMeshProUGUI countTextBox;

    List<LevelInfo> levels = new List<LevelInfo>();
    List<LevelParser.LevelStrings> paths = new List<LevelParser.LevelStrings>();

    int currentIndex = 0;

    bool levelsParsed = false;

    bool joystickEventConsumed = false;

    bool noLevelsComplete = false;



    public class Events
    {
        [Serializable]
        public class LevelDeleted : UnityEvent { }
    }

    /// <summary>
    /// Event invoked when level deleted
    /// </summary>
    public Events.LevelDeleted OnLevelDeleted;

    // Start is called before the first frame update
    void Start()
    {
        HideCanvas();
    }

    void Update()
    {
        if (deleteLevel.activeInHierarchy)
        {
            if (levels.Count == 0)
            {
                if (!noLevelsComplete)
                {
                    ShowNoLevels();
                }
                return;
            }
            else
            {
                levelSelectButton.gameObject.SetActive(true);
            }

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

    }

    void ShowNoLevels()
    {
        noLevelsComplete = true;
        DisableArrows();
        levelSelectButton.gameObject.SetActive(false);
        backButton.Select();
        countTextBox.text = "0/0";
        levelNameTextBox.text = "There are no levels to delete.";
        previewer.DisableCurrentWallsAndClearList();
    }

    void DisableArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
    }

    public void SelectAppropriateButton()
    {
        if (levels.Count == 0)
        {
            backButton.Select();
        }
        else
        {
            levelSelectButton.Select();
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

    public void DeleteSelectedLevel()
    {
        //TODO: Load selected level
        try
        {
            LevelParser.LevelStrings strings = paths[currentIndex];

            File.Delete(strings.LevelPath);
            LevelParser.Parser.DirectoryLevelDictionary.Remove(strings);
            LevelParser.Parser.LevelDictionary.Remove(strings.LevelName);
            paths.RemoveAt(currentIndex);
            levels.RemoveAt(currentIndex);
            OnLevelDeleted.Invoke();
            if (levels.Count == 0)
            {
                ShowNoLevels();
            }
            else if (currentIndex < levels.Count)
            {
                ShowLevelData();
            }
            else
            {
                PreviousLevel();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("ERROR: While deleting " + e);
        }
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
            paths.Clear();
            foreach (var info in LevelParser.Parser.DirectoryLevelDictionary)
            {
                levels.Add(info.Value);
                paths.Add(info.Key);
            }

            levels.Sort((a, b) => a.Name.CompareTo(b.Name));
            paths.Sort((a, b) => a.LevelName.CompareTo(b.LevelName)); //NOTE: This only works if the names are the same in each list
        }
    }


    /// <summary>
    /// Shows the level select canvas, and selects the invisible level select button
    /// </summary>
    public void ShowCanvas()
    {
        ParseAndSortDictionary();
        EnableArrows();
        deleteLevel.gameObject.SetActive(true);

        if (levels.Count == 0)
        {
            ShowNoLevels();
        }
        else
        {
            ShowLevelData();
            levelSelectButton.Select();
        }
    }

    /// <summary>
    /// Hides the level select canvas and selects the start button
    /// </summary>
    public void HideCanvas()
    {
        confirmation.gameObject.SetActive(false);
        noLevelsComplete = false;
        deleteLevel.gameObject.SetActive(false);
        deleteButton.Select();
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
        ShowLevelData();
    }

    void ShowLevelData()
    {
        if (currentIndex < levels.Count)
        {
            previewer.GenerateLevel(levels[currentIndex]);
            countTextBox.text = "" + (currentIndex + 1) + "/" + levels.Count;
            levelNameTextBox.text = levels[currentIndex].Name;
        }
        else
        {
            Debug.LogError("Index out of range. Count: " + levels.Count + " Index: " + currentIndex);
        }
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
        ShowLevelData();
    }
}
