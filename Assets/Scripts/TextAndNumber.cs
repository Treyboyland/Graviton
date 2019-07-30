using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAndNumber : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textBox;

    [SerializeField]
    string initialText;

    public void SetNumber(int num)
    {
        textBox.text = initialText + string.Format("{0,7}", "" + num);
    }
}
