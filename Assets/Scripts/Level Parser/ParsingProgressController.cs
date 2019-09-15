using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParsingProgressController : MonoBehaviour
{
    [SerializeField]
    GameObject levelProgressCanvas;

    // Start is called before the first frame update
    void Start()
    {
        ShowParsingProgressCanvas();
    }

    /// <summary>
    /// Show loading progress
    /// </summary>
    public void ShowParsingProgressCanvas()
    {
        levelProgressCanvas.SetActive(true);
        LevelParser.Parser.ParseLevelsAsync();
    }

    /// <summary>
    /// Loads the title screen
    /// </summary>
    public void LoadTitleScreen()
    {
        SceneLoader.Loader.LoadLoadingScene();
    }
}
