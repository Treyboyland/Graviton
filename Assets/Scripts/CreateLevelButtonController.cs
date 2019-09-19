using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevelButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject createLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        createLevelButton.SetActive(GameConfigReader.ConfigReader != null ? GameConfigReader.ConfigReader.Configuration.AllowLevelCreation : true);
    }


}
