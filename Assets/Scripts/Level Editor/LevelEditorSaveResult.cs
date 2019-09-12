using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelEditorSaveResult : MonoBehaviour
{
    [SerializeField]
    LevelEditorSaveButton saveButton;

    [SerializeField]
    TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        saveButton.OnLevelCreated.AddListener(ActivateTextBox);
    }

    void ActivateTextBox(string name)
    {
        textBox.text = "Level Saved as " + name;
        textBox.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        textBox.gameObject.SetActive(false);
    }
}
