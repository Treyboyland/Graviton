using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MusicController : MonoBehaviour
{
    [Serializable]
    public struct MusicWithCombo
    {
        public AudioSource audioSource;
        public int combo;
    }

    public class Events
    {
        [Serializable]
        public class SetComboLevel : UnityEvent<int> { }
    }

    [SerializeField]
    List<MusicWithCombo> music;

    static MusicController _instance;

    public static MusicController Instance
    {
        get
        {
            return _instance;
        }
    }

    public Events.SetComboLevel OnSetComboLevel;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayAll();
        UnMuteSounds(0);

        OnSetComboLevel.AddListener(UnMuteSounds);
        SceneManager.sceneLoaded += (unused, alsoUnused) => UnMuteSounds(0);
        BaseGameManager.Manager.OnPlayerComboUpdated.AddListener(UnMuteSounds);
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
        //Debug.LogWarning("Combo Music: " + combo);
        //NOTE: This is incredibly inefficient
        foreach (MusicWithCombo m in music)
        {
            m.audioSource.mute = !(m.combo <= combo);
        }
    }


}
