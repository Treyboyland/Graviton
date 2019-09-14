using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChooseLevelController : MonoBehaviour
{
    [SerializeField]
    GameObject buttonHolder;

    [SerializeField]
    GameObject levelCanvas;

    [SerializeField]
    Button backButton;

    [SerializeField]
    Button startButton;

    [SerializeField]
    LevelButton levelButtonPrefab;

    LevelParser parser;

    List<LevelButton> levelButtons;

    /// <summary>
    /// True if the buttons have been created
    /// </summary>
    bool areButtonsCreated = false;

    // Start is called before the first frame update
    void Start()
    {
        parser = LevelParser.Parser;
        HideLevelCanvas();
    }

    public void ShowLevelCanvas()
    {
        levelCanvas.SetActive(true);
    }

    public void HideLevelCanvas()
    {
        levelCanvas.SetActive(false);
        startButton.Select();
    }

    public void ShowLevelSelection()
    {
        if (!parser.AreLevelsParsed)
        {
            parser.ParseLevels();
        }
        if (!areButtonsCreated)
        {
            InstantiateBoxes();
        }

        ShowLevelCanvas();
        if (levelButtons.Count != 0)
        {
            levelButtons[0].Select();
        }
        else
        {
            backButton.Select();
        }
    }

    void ExplictlySetLevelButtonNavigation(List<Button> buttons)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Navigation nav = buttons[i].navigation;
            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnLeft = i != 0 ? buttons[i-1] : buttons[i];
            nav.selectOnRight = i != buttons.Count - 1 ? buttons[i+1] : buttons[i];
            nav.selectOnDown = null;
            nav.selectOnUp = null;
            buttons[i].navigation = nav;
        }
    }

    void InstantiateBoxes()
    {
        levelButtons = new List<LevelButton>(parser.LevelDictionary.Keys.Count);

        foreach (string name in parser.LevelDictionary.Keys)
        {
            LevelButton button = Instantiate(levelButtonPrefab, buttonHolder.transform);
            button.ButtonName = name;
            button.LevelData = parser.LevelDictionary[name];
            levelButtons.Add(button);

        }

        backButton.transform.SetAsLastSibling();

        List<Button> buttons = new List<Button>(levelButtons.Select(x => x.ButtonObject));
        buttons.Add(backButton);

        ExplictlySetLevelButtonNavigation(buttons);

        areButtonsCreated = true;
    }
}
