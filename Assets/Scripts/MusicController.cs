using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    public struct MusicWithCombo
    {
        public AudioSource audioSource;
        public int combo;
    }

    [SerializeField]
    List<MusicWithCombo> music;

    static MusicController _instance;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayAll();
        UnMuteSounds(0);

        SceneManager.sceneLoaded += (unused, alsoUnused) => UnMuteSounds(0);
        GameManager.Manager.OnPlayerComboUpdated.AddListener(UnMuteSounds);
        //GameManager.Manager.OnResetPlayerCombo.AddListener(() => UnMuteSounds(0));
    }


    void PlayAll()
    {
        foreach (MusicWithCombo m in music)
        {
            m.audioSource.Play();
        }
    }

    void UnMuteSounds(int combo)
    {
        //NOTE: This is incredibly inefficient
        foreach (MusicWithCombo m in music)
        {
            m.audioSource.mute = !(m.combo <= combo);
        }
    }


}
