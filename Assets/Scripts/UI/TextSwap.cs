using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;

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

    PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
        input = GameObject.FindAnyObjectByType<PlayerInput>();
    }

    private void Update()
    {
        //NOTE: This doesn't take into account connected joysticks that may not be in use
        bool prev = UsingKeyboard;
        UsingKeyboard = input != null ? input.currentControlScheme == "Keyboard&Mouse" : true;
        if (prev != UsingKeyboard)
        {
            SetText();
        }
    }

    void SetText()
    {
        textBox.text = UsingKeyboard ? keyboardText : joystickText;
    }
}
