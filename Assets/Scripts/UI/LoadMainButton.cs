using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMainButton : MonoBehaviour
{
    [SerializeField]
    Button button;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(SceneLoader.Loader.LoadLoadingScene);
    }
}
