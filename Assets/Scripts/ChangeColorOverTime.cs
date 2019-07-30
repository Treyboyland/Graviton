using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorOverTime : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Color startColor;

    [SerializeField]
    Color endColor;

    [SerializeField]
    float secondsToTake;

    bool isFadeFinished;

    public bool IsFadeFinished
    {
        get
        {
            return isFadeFinished;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ExecuteFade());
    }

    IEnumerator ExecuteFade()
    {
        isFadeFinished = false;
        image.color = startColor;
        float elapsedTime = 0;

        while (elapsedTime < secondsToTake)
        {
            image.color = Color.Lerp(startColor, endColor, elapsedTime / secondsToTake);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        image.color = endColor;

        isFadeFinished = true;
    }

    public void SetToEndColor()
    {
        StopAllCoroutines();
        image.color = endColor;
    }
}
