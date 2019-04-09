using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRng : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(UnityEngine.Random.Range(0, 10000));
    }
}
