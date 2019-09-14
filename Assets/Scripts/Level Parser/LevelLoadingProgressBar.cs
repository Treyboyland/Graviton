using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoadingProgressBar : MonoBehaviour
{
    [SerializeField]
    ParsingProgressController progressController;

    [SerializeField]
    Image progressBar;

    bool areLevelsLoaded = false;

    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarning(LevelParser.Parser.Progress);
        progressBar.fillAmount = LevelParser.Parser.Progress;
        if (LevelParser.Parser.AreLevelsParsed && !areLevelsLoaded)
        {
            areLevelsLoaded = true;
            progressController.ShowLevelSelect();
        }
    }

    private void OnEnable()
    {
        progressBar.fillAmount = 0;
    }

    private void OnDisable()
    {
        areLevelsLoaded = false;
    }
}
