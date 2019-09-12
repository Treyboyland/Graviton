using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseLevelControllerNew : MonoBehaviour
{
    [SerializeField]
    GameObject chooseLevelCanvas;

    [SerializeField]
    GameObject levelSelectButton;

    [SerializeField]
    LevelPreviewer previewer;

    List<LevelInfo> levels = new List<LevelInfo>();

    int currentIndex = 0;

    bool levelsParsed = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    void HandleLevelMove()
    {

    }

    void ParseAndSortDictionary()
    {
        levels.Clear();
        foreach (LevelInfo info in LevelParser.Parser.LevelDictionary.Values)
        {
            levels.Add(info);
        }

        levels.Sort((a, b) => a.Name.CompareTo(b.Name));
    }

    bool IsLeftArrowActive()
    {
        return currentIndex != 0;
    }

    bool IsRightArrowActive()
    {
        return currentIndex != levels.Count - 1;
    }

    public void ShowCanvas()
    {
        previewer.GenerateLevel(levels[currentIndex]);
        chooseLevelCanvas.gameObject.SetActive(true);
    }

    public void HideCanvas()
    {
        chooseLevelCanvas.gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        if (currentIndex + 1 < levels.Count)
        {
            currentIndex++;
            previewer.GenerateLevel(levels[currentIndex]);
        }
    }

    public void PreviousLevel()
    {
        if (currentIndex - 1 > 0)
        {
            currentIndex--;
            previewer.GenerateLevel(levels[currentIndex]);
        }
    }

}
