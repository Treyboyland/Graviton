using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject exitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        exitGameButton.SetActive(GameConfigReader.ConfigReader != null ? GameConfigReader.ConfigReader.Configuration.AllowExiting : true);
    }


}
