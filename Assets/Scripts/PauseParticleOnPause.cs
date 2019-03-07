using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseParticleOnPause : MonoBehaviour
{
    [SerializeField]
    ParticleSystem ps;

    bool wasPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Manager.OnGamePaused.AddListener(TogglePause);
    }

    void TogglePause(bool paused)
    {
        if(paused)
        {
            wasPlaying = ps.isPlaying;
            ps.Pause();
        }
        else if(wasPlaying)
        {
            ps.Play();
        }
    }
}
