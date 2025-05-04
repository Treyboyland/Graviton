using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ChooseLevelControllerNew : MonoBehaviour
{
    [SerializeField]
    DeleteLevelController deleteLevelController;

    [SerializeField]
    GameObject chooseLevelCanvas;

    [SerializeField]
    Button levelSelectButton;

    [SerializeField]
    Button backButton;

    [SerializeField]
    Button startButton;

    [SerializeField]
    LevelPreviewer previewer;

    [SerializeField]
    List<GameObject> arrows;

    [SerializeField]
    float secondsBetweenLevelChanges;

    [SerializeField]
    TextMeshProUGUI levelNameTextBox;

    [SerializeField]
    TextMeshProUGUI countTextBox;

    List<LevelInfo> levels = new List<LevelInfo>();

    int currentIndex = 0;

    bool levelsParsed = false;

    bool noLevelsComplete = false;

    float elapsedLevelMoveDelay = 0;
    Vector2 currentMovementVector;

    //NOTE: This class shares *a lot* of code with the delete canvas...They should probably be extensions of each other or something

    // Start is called before the first frame update
    void Start()
    {
        deleteLevelController.OnLevelDeleted.AddListener(() =>
        {
            currentIndex = 0;
            levelsParsed = false;
        });
        HideCanvas();
    }

    void Update()
    {
        if (chooseLevelCanvas.activeInHierarchy)
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
                elapsedLevelMoveDelay = Mathf.Min(elapsedLevelMoveDelay + Time.deltaTime, secondsBetweenLevelChanges);
                EnableArrows();
                ChangeLevel();
            }
            else
            {
                DisableArrows();
                elapsedLevelMoveDelay = secondsBetweenLevelChanges;
            }
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

    /// <summary>
    /// Should listen to move event
    /// </summary>
    /// <param name="movementVector"></param>
    public void HandleLevelMove(Vector2 movementVector)
    {
        if (EventSystem.current.currentSelectedGameObject != levelSelectButton.gameObject)
        {
            return;
        }

        movementVector = movementVector.IsolateGreater();

        if (currentMovementVector != movementVector)
        {
            currentMovementVector = movementVector;
            elapsedLevelMoveDelay = secondsBetweenLevelChanges;
        }
        if (currentMovementVector != Vector2.zero)
        {
            ChangeLevel();
        }
    }

    void ChangeLevel()
    {
        if (elapsedLevelMoveDelay < secondsBetweenLevelChanges)
        {
            return;
        }
        if (currentMovementVector.x < 0)
        {
            elapsedLevelMoveDelay = 0;
            PreviousLevel();
        }
        else if (currentMovementVector.x > 0)
        {
            elapsedLevelMoveDelay = 0;
            NextLevel();
        }
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

    void ShowNoLevels()
    {
        //TODO: Show no level notification
        noLevelsComplete = true;
        DisableArrows();
        levelSelectButton.gameObject.SetActive(false);
        backButton.Select();
        countTextBox.text = "0/0";
        levelNameTextBox.text = "There are no levels to play." +
         (GameConfigReader.ConfigReader.Configuration.AllowLevelCreation ? " Create some!" : "");
        previewer.DisableCurrentWallsAndClearList();
    }


    /// <summary>
    /// Shows the level select canvas, and selects the invisible level select button
    /// </summary>
    public void ShowCanvas()
    {
        ParseAndSortDictionary();
        EnableArrows();
        chooseLevelCanvas.gameObject.SetActive(true);

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
        noLevelsComplete = false;
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
            Debug.LogError("ERROR: " + gameObject.name + ": Out of range. Count: " + levels.Count + " Index: " + currentIndex);
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
