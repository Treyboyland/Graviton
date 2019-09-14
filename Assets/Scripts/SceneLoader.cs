using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class SceneLoader : MonoBehaviour
{
    bool isLoading = false;

    static SceneLoader _instance;

    [SerializeField]
    string mainGameSceneName;

    [SerializeField]
    string loadingSceneName;

    [SerializeField]
    string editorSceneName;

    string loadNameEnd;

    public static SceneLoader Loader
    {
        get
        {
            return _instance;
        }
    }

    public class Events
    {
        [Serializable]
        public class SceneChanged : UnityEvent { }
    }

    public Events.SceneChanged OnSceneChanged;

    void Awake()
    {
        if (_instance != null && this != _instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        loadNameEnd = loadingSceneName.Substring(loadingSceneName.LastIndexOf('/') + 1);
    }

    public void LoadMainGameScene()
    {
        if (!isLoading)
        {
            StartCoroutine(WaitUntilLoaded(mainGameSceneName));
        }
    }

    public bool IsLoadingSceneLoaded()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.LogWarning(currentScene.name);
        return currentScene.name == loadingSceneName;
    }

    public void LoadLoadingScene()
    {
        if (!isLoading)
        {
            StartCoroutine(WaitUntilLoaded(loadingSceneName));
        }
    }

    public void LoadEditorScene()
    {
        if(!isLoading)
        {
            StartCoroutine(WaitUntilLoaded(editorSceneName));
        }
    }

    IEnumerator WaitUntilLoaded(string sceneName)
    {
        isLoading = true;
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!op.isDone)
        {
            yield return null;
        }

        isLoading = false;
        OnSceneChanged.Invoke();
    }
}
