using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    bool shouldTrack;

    public bool ShouldTrack
    {
        get
        {
            return shouldTrack;
        }
        set
        {
            shouldTrack = value;
        }
    }

    GameObject objectToTrack;

    public GameObject ObjectToTrack
    {
        get
        {
            return objectToTrack;
        }
        set
        {
            objectToTrack = value;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if(ShouldTrack && ObjectToTrack != null)
        {
            transform.position = ObjectToTrack.transform.position;
        }
    }
}
