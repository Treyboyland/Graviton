using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseGameButton : MonoBehaviour
{
    [SerializeField]
    Button button;


    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        button.onClick.AddListener(() => UnityEditor.EditorApplication.isPlaying = false);
#elif !UNITY_WEBGL
        button.onClick.AddListener(() => Application.Quit());
#endif
    }
}
