using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class SetTextFromEvent : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textbox;

    public void SetText(string text)
    {
        textbox.text = text;
    }
}
