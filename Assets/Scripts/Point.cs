using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    int pointsAwarded;

    public int PointsAwarded
    {
        get
        {
            return pointsAwarded;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.LogWarning("Trigger entered");
        if(string.Compare(other.gameObject.tag, "Player", true) == 0)
        {
            BaseGameManager.Manager.OnPointsReceived.Invoke(pointsAwarded);
            gameObject.SetActive(false);
        }
    }
}
