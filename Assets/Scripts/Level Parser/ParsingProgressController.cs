using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsingProgressController : MonoBehaviour
{
    [SerializeField]
    ChooseLevelControllerNew chooseLevelNew;

    [SerializeField]
    GameObject levelProgressCanvas;

    // Start is called before the first frame update
    void Start()
    {
        levelProgressCanvas.SetActive(false);
    }

    public void ShowParsingProgressCanvas()
    {
        if (!LevelParser.Parser.AreLevelsParsed)
        {
            levelProgressCanvas.SetActive(true);
            LevelParser.Parser.ParseLevelsAsync();
        }
        else
        {
            chooseLevelNew.ShowCanvas();
        }
    }



    public void ShowLevelSelect()
    {
        levelProgressCanvas.SetActive(false);
        chooseLevelNew.ShowCanvas();
    }
}
