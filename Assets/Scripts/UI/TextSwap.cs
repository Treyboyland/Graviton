using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextSwap : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textBox;

    /// <summary>
    /// Text used with keyboard input
    /// </summary>
    [SerializeField]
    [TextArea]
    string keyboardText;

    /// <summary>
    /// Text used with joystick input
    /// </summary>
    [SerializeField]
    [TextArea]
    string joystickText;

    /// <summary>
    /// True if we are currently using the keyboard
    /// </summary>
    bool usingKeyboard = true;

    /// <summary>
    /// True if we are currently using the keyboard
    /// </summary>
    bool UsingKeyboard
    {
        get
        {
            return usingKeyboard;
        }
        set
        {
            if (usingKeyboard != value)
            {
                usingKeyboard = value;
                SetText();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    private void Update()
    {
        //NOTE: This doesn't take into account connected joysticks that may not be in use
        UsingKeyboard = Input.GetJoystickNames().Length == 0;
    }

    void SetText()
    {
        textBox.text = UsingKeyboard ? keyboardText : joystickText;
    }
}
