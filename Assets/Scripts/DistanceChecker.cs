using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField]
    Transform other;

    
    // Update is called once per frame
    void Update()
    {
        CalculateDistance();
    }

    void CalculateDistance()
    {
        Debug.Log(Vector2.Distance(transform.position, other.position));
    }
}
