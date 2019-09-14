using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelButtonNew : MonoBehaviour
{
    [SerializeField]
    ChooseLevelControllerNew controllerNew;

    [SerializeField]
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(LoadSelected);
    }

    // Update is called once per frame
    void LoadSelected()
    {
        controllerNew.LoadSelectedLevel();
    }
}
