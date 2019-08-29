using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    [SerializeField]
    Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause") && !menu.activeInHierarchy)
        {
            menu.SetActive(true);
            backButton.Select();
        }       
    }
}
