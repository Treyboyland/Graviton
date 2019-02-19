using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        BaseGameManager.Manager.OnScoreUpdated.AddListener(SetScore);
        SetScore(0);
    }

    void SetScore(int score)
    {
        
        textBox.text = "Score: " + score;
    }
}
