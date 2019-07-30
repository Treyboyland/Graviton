using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    [SerializeField]
    HighScoreKeeper scoreKeeper;

    [SerializeField]
    GameObject highScoreCanvas;

    [SerializeField]
    ChangeColorOverTime highScoreBackground;

    [SerializeField]
    TextAndNumber playerScoreText;

    [SerializeField]
    TextAndNumber highScoreText;

    [SerializeField]
    GameObject highScoreCelebration;

    [SerializeField]
    GameObject errorText;

    [SerializeField]
    Button restartButton;

    [SerializeField]
    Button exitButton;

    [SerializeField]
    Player player;


    bool canSkip = false;

    bool isHighScore = false;

    int highScoreToShow = 0;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnGameOver.AddListener(() => StartCoroutine(EndGame()));
        scoreKeeper.OnHighScoreSaveFail.AddListener(() => errorText.SetActive(true));
        scoreKeeper.OnNewHighScore.AddListener((value, highScore) => {
            isHighScore = value;
            highScoreToShow = highScore;
        });

        DisableEverything();
        errorText.SetActive(false);
    }

    void DisableEverything()
    {
        highScoreCanvas.SetActive(false);
        playerScoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);
        highScoreCelebration.SetActive(false);
        restartButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    void SkipStuff()
    {
        StopAllCoroutines();
        highScoreCanvas.SetActive(true);
        highScoreBackground.SetToEndColor();
        playerScoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);

    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1.0f);

        highScoreCanvas.SetActive(true);

        while (!highScoreBackground.IsFadeFinished)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        playerScoreText.SetNumber(player.Score);
        highScoreText.SetNumber(highScoreToShow);

        playerScoreText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        highScoreText.gameObject.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(0.5f);

        if (isHighScore)
        {
            yield return new WaitForSeconds(0.5f);
            highScoreCelebration.SetActive(true);
        }

        restartButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);

        restartButton.Select();
    }
}
