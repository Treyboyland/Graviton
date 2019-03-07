using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(()=> SceneLoader.Loader.LoadMainGameScene());    
    }
}
