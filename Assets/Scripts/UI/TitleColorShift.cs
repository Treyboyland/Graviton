using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleColorShift : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textBox;

    [SerializeField]
    List<Color> colors;

    [SerializeField]
    float secondsToShift;

    [SerializeField]
    float secondsToStayColor;

    [SerializeField]
    float secondsToReturn;

    [SerializeField]
    float secondsToStayWhite;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        ShuffleColors();
        StartCoroutine(ShiftColors());
    }

    void ShuffleColors()
    {
        for (int i = 0; i < colors.Count; i++)
        {
            int chosenIndex = UnityEngine.Random.Range(i, colors.Count);
            Color c = colors[chosenIndex];
            colors[chosenIndex] = colors[i];
            colors[i] = c;
        }
        index = 0;
    }

    /// <summary>
    /// Waits for a given amount of seconds
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator WaitForTime(float seconds)
    {
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        while (timer.Elapsed.TotalSeconds < seconds)
        {
            yield return null;
        }

    }

    void SetColor(Color c)
    {
        textBox.color = c;
    }

    /// <summary>
    /// Shifts from start color to end color in seconds
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator ColorShift(Color start, Color end, float seconds)
    {
        System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
        timer.Start();

        while (timer.Elapsed.TotalSeconds < seconds)
        {
            float time = (float)timer.Elapsed.TotalSeconds;

            Color color;
            color.r = Mathf.Lerp(start.r, end.r, time / seconds);
            color.g = Mathf.Lerp(start.g, end.g, time / seconds);
            color.b = Mathf.Lerp(start.b, end.b, time / seconds);
            color.a = Mathf.Lerp(start.a, end.a, time / seconds);

            SetColor(color);
            yield return null;
        }
    }

    /// <summary>
    /// Shifts the colors of the title screen
    /// </summary>
    /// <returns></returns>
    IEnumerator ShiftColors()
    {
        while (true)
        {
            Color chosenColor = colors[index];
            yield return StartCoroutine(WaitForTime(secondsToStayWhite));

            yield return StartCoroutine(ColorShift(Color.white, chosenColor, secondsToShift));

            yield return StartCoroutine(WaitForTime(secondsToStayColor));

            yield return StartCoroutine(ColorShift(chosenColor, Color.white, secondsToShift));
            index = (index + 1) % colors.Count;
            if (index == 0)
            {
                ShuffleColors();
            }

        }
    }
}
