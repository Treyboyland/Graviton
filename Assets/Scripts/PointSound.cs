using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        BaseGameManager.Manager.OnPointsReceived.AddListener((points) => {
            audioSource.PlayOneShot(clip);
        });
    }
}
