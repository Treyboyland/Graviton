using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    GameObject pauseCanvas;

    [SerializeField]
    Button resumeButton;

    BaseGameManager manager;    

    bool isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        ReAddListener();
        manager.OnGamePaused.AddListener(HandlePausing);
        manager.OnGameOver.AddListener(() => isGameOver = true);
        pauseCanvas.SetActive(false);
    }

    void ReAddListener()
    {
        if(manager == null)
        {
            manager = BaseGameManager.Manager;
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("Pause") && !isGameOver)
        {
            if(!pauseCanvas.activeInHierarchy)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    void HandlePausing(bool isPaused)
    {
        if(isPaused)
        {
            //Time.timeScale = 0;
            pauseCanvas.SetActive(true);
            resumeButton.Select();
        }
        else
        {
            //Time.timeScale = 1;
            pauseCanvas.SetActive(false);
        }
    }

    public void PauseGame()
    {
        ReAddListener();
        manager.OnGamePaused.Invoke(true);
    }

    public void UnpauseGame()
    {
        ReAddListener();
        manager.OnGamePaused.Invoke(false);
    }
}
