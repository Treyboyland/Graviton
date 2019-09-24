using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteLevelButtonController : MonoBehaviour
{
    [SerializeField]
    GameObject deleteLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        deleteLevelButton.SetActive(GameConfigReader.ConfigReader != null ? GameConfigReader.ConfigReader.Configuration.AllowLevelDeletion : true);
    }
}
