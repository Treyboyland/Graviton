using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColorOverTime : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sprite;

    [SerializeField]
    Gradient gradient;

    [SerializeField]
    float secondsPerLoop;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGradient()
    {
        StopGradient();
        StartCoroutine(GradientLoop());
    }

    public void StopGradient()
    {
        StopAllCoroutines();
    }

    public void SetToStartColor()
    {
        StopGradient();
        sprite.color = gradient.Evaluate(0);
    }

    public void SetToEndColor()
    {
        StopGradient();
        sprite.color = gradient.Evaluate(1);
    }

    private void OnValidate()
    {
        if (sprite == null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    IEnumerator GradientLoop()
    {
        while (true)
        {
            float time = 0;
            sprite.color = gradient.Evaluate(time / secondsPerLoop);
            while (time < secondsPerLoop)
            {
                time += Time.deltaTime;
                sprite.color = gradient.Evaluate(time / secondsPerLoop);
                yield return null;
            }
            sprite.color = gradient.Evaluate(time / secondsPerLoop);
            yield return null;
        }
    }
}
