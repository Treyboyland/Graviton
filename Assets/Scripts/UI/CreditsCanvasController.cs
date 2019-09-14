using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsCanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject creditsCanvas;

    [SerializeField]
    Button creditsButton;

    [SerializeField]
    Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        creditsCanvas.gameObject.SetActive(false);
    }

    public void HideCanvas()
    {
        creditsCanvas.gameObject.SetActive(false);
        creditsButton.Select();
    }

    public void ShowCanvas()
    {
        creditsCanvas.gameObject.SetActive(true);
        backButton.Select();
    }
}
