using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    Button button;

    [SerializeField]
    TextMeshProUGUI textBox;

    string buttonName;

    public string ButtonName
    {
        get
        {
            return buttonName;
        }
        set
        {
            buttonName = value;
        }
    }

    LevelInfo levelInfo;

    public LevelInfo LevelData
    {
        get
        {
            return levelInfo;
        }
        set
        {
            levelInfo = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetData();
        button.onClick.AddListener(() => LevelParser.Parser.ChosenLevel = buttonName);
    }

    void SetData()
    {
        textBox.text = buttonName;
    }
}
